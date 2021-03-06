﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Braintree;
using Utility;
using Platform.App_Code;

namespace Platform.Controllers
{
    public class PaymentController : Controller
    {
        public BraintreeConfiguration config = new BraintreeConfiguration();

        public static readonly TransactionStatus[] transactionSuccessStatuses = {
                                                                                    TransactionStatus.AUTHORIZED,
                                                                                    TransactionStatus.AUTHORIZING,
                                                                                    TransactionStatus.SETTLED,
                                                                                    TransactionStatus.SETTLING,
                                                                                    TransactionStatus.SETTLEMENT_CONFIRMED,
                                                                                    TransactionStatus.SETTLEMENT_PENDING,
                                                                                    TransactionStatus.SUBMITTED_FOR_SETTLEMENT
                                                                                };

        private static readonly string SUBSCRIPTION_PLAN_ID = "fbqg";


    public ActionResult Index()
        {


           // var gateway = config.GetGateway();
           // var clientToken = gateway.ClientToken.generate();
         //   ViewBag.ClientToken = clientToken;
            return View();
        }

        public ActionResult New()
        {


            var gateway = config.GetGateway();
            var clientToken = gateway.ClientToken.generate();
            ViewBag.ClientToken = clientToken;
            return View();
        }

        //[HttpPost]
        //[MultipleButton(Name = "action", Argument = "Subscription")]
        public ActionResult Subscription()
        {
            var gateway = config.GetGateway();

            var nonce = Request["payment_method_nonce"];
            var request = new SubscriptionRequest
            {
                PaymentMethodToken = "5n4wnm",
                PlanId = SUBSCRIPTION_PLAN_ID
            };


            Result<Subscription> result = gateway.Subscription.Create(request);

            if (result.IsSuccess())
            {
//                SubscriptionRequest subscription = request.PlanId;
                return RedirectToAction("Show", new { id = request.PlanId });
            }
            else if (result.Subscription != null)
            {
                return RedirectToAction("Show", new { id = result.Transaction.Id });
            }
            else
            {
                string errorMessages = "";
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
                }

                Utility.Logging.Log(errorMessages, "PaymentController");
                TempData["Flash"] = errorMessages;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Transaction")]
        public ActionResult Transaction()
        {
            var gateway = config.GetGateway();
            Decimal amount;

            try
            {
                amount = Convert.ToDecimal(Request["amount"]);
            }
            catch (FormatException e)
            {
                Utility.Logging.Log("Error: 81503: Amount is an invalid format.", "PaymentController");
                TempData["Flash"] = "Error: 81503: Amount is an invalid format.";
                return RedirectToAction("New");
            }

            var nonce = Request["payment_method_nonce"];
            var request = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = nonce
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                Transaction transaction = result.Target;
                return RedirectToAction("Show", new { id = transaction.Id });
            }
            else if (result.Transaction != null)
            {
                return RedirectToAction("Show", new { id = result.Transaction.Id });
            }
            else
            {
                string errorMessages = "";
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
                }

                Utility.Logging.Log(errorMessages, "PaymentController");
                TempData["Flash"] = errorMessages;
                return RedirectToAction("New");
            }

        }

        public ActionResult Show(String id)
        {
            var gateway = config.GetGateway();
            Transaction transaction = gateway.Transaction.Find(id);

            if (transactionSuccessStatuses.Contains(transaction.Status))
            {
                TempData["header"] = "Sweet Success!";
                TempData["icon"] = "success";
                TempData["message"] = "Your test transaction has been successfully processed. See the Braintree API response and try again.";
            }
            else
            {
                TempData["header"] = "Transaction Failed";
                TempData["icon"] = "fail";
                var message = "Your test transaction has a status of " + transaction.Status + ". See the Braintree API response and try again.";

                TempData["message"] = message;
                Logging.Log(message, "PaymentController");

            };

            ViewBag.Transaction = transaction;
            return View();
        }


        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public class MultipleButtonAttribute : ActionNameSelectorAttribute
        {
            public string Name { get; set; }
            public string Argument { get; set; }

            public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
            {
                var isValidName = false;
                var keyValue = string.Format("{0}:{1}", Name, Argument);
                var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

                if (value != null)
                {
                    controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
                    isValidName = true;
                }

                return isValidName;
            }

        }

    }

    


}