// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with EmptyBot .NET Template version v4.7.0

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace HCRDialogs
{
   internal class GreetingDialog : ComponentDialog
   {
      public const string WhatCrimeMessage = "What type of hate crime are you reporting?";

      public GreetingDialog()
         : base("GreetingDialog")
      {

         var waterfallSteps = new WaterfallStep[]
         {
            PromptForCrimeType,
            PromptForCrimeTime,
            PromptForBehaviourDescription,
            FinishAsync,
         };

         //Register the waterfall dialog. 
         AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
         //Register text prompts, so we can re-use them. 
         AddDialog(new TextPrompt(nameof(TextPrompt)));
         //Make sure we start with the waterfall dialog
         InitialDialogId = nameof(WaterfallDialog);
      }

      private async Task<DialogTurnResult> PromptForBehaviourDescription(WaterfallStepContext stepContext, CancellationToken cancellationToken)
      {
         stepContext.Values["CrimeTime"] = (string)stepContext.Result;

         return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
         {
            Prompt = MessageFactory.Text($"what behaviour did you see?")
         },
                cancellationToken);
      }

      private async Task<DialogTurnResult> PromptForCrimeTime(WaterfallStepContext stepContext, CancellationToken cancellationToken)
      {
         stepContext.Values["CrimeType"] = (string)stepContext.Result;

         return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
         {
            Prompt = MessageFactory.Text($"When did the incident happen? You can give the exact time or the number of minutes into the game if you'd prefer.")
         },
                cancellationToken);
      }

      private async Task<DialogTurnResult> FinishAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
      {
         var greetingResult = new GreetingResult
         {
            CrimeBehaviour = (string)stepContext.Result,
            CrimeTime = (string)stepContext.Values["CrimeTime"],
            CrimeType = (string)stepContext.Values["CrimeType"],
         };

         return await stepContext.EndDialogAsync(greetingResult, cancellationToken: cancellationToken);
      }

      private async Task<DialogTurnResult> PromptForCrimeType(WaterfallStepContext stepContext, CancellationToken cancellationToken)
      {
         return await stepContext.PromptAsync(nameof(TextPrompt),
              new PromptOptions
              {
                 Prompt = MessageFactory.Text(WhatCrimeMessage),
              },
              cancellationToken);
      }
   }
}