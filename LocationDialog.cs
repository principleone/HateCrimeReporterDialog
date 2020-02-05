// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with EmptyBot .NET Template version v4.7.0

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;

namespace HCRDialogs
{
   internal class LocationDialog : ComponentDialog
   {
      public LocationDialog()
      : base(nameof(LocationDialog))
      {
         var waterfallSteps = new WaterfallStep[]
         {
          ChooseClubStep,
         };

         //Register the waterfall dialog. 
         AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
         //Register text prompts, so we can re-use them. 
         AddDialog(new TextPrompt(nameof(TextPrompt)));

         AddDialog(new ChoicePrompt("ClubPrompt"));
         //Make sure we start with the waterfall dialog
         InitialDialogId = nameof(WaterfallDialog);
      }

      private async Task<DialogTurnResult> ChooseClubStep(WaterfallStepContext stepContext, CancellationToken cancellationToken)
      {
         return await stepContext.PromptAsync("ClubPrompt", new PromptOptions
         {
            Choices = ChoiceFactory.ToChoices(new List<string> { "Vauxhall FC", "London United" }),
            Prompt = MessageFactory.Text("What club are you at?"),
            RetryPrompt = MessageFactory.Text("Please select one of the clubs I know")
         });
      }
   }
}