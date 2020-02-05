// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with EmptyBot .NET Template version v4.7.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;


namespace HCRDialogs
{
   public class HCRDialogBot : IBot
   {
      public const string Greeting = "Hi! You are talking to the Hate Crime Report Bot. Give me the details of what happened, and I will let the club know so they can take action!";
      public const string ThankYou = "Thank you, I have stored all that information. Let’s move onto where this happened.";

      private readonly DialogSet dialogSet;
      private readonly ConversationState conversationState;

      public HCRDialogBot(ConversationState conversationState)
      {
         dialogSet = new DialogSet(conversationState.CreateProperty<DialogState>("state"));
         dialogSet.Add(new GreetingDialog());
         this.conversationState = conversationState;
      }

      public async Task OnTurnAsync(
          ITurnContext turnContext,
          CancellationToken cancellationToken = default(CancellationToken))
      {
         switch (turnContext.Activity.Type)
         {
            // On a message from the user:
            case ActivityTypes.Message:

               var dc = await dialogSet.CreateContextAsync(turnContext, cancellationToken);
               if (dc.ActiveDialog is null)
               {
                  await turnContext.SendActivityAsync(MessageFactory.Text(Greeting), cancellationToken);
                  await dc.BeginDialogAsync(nameof(GreetingDialog), null, cancellationToken);
               }
               else
               {
                  var continueResult = await dc.ContinueDialogAsync(cancellationToken);
                  if (continueResult.Status == DialogTurnStatus.Complete)
                  {
                     var greetingResult = (GreetingResult)continueResult.Result;
                     await turnContext.SendActivityAsync(MessageFactory.Text($"You have reported a crime of {greetingResult.CrimeType}."));
                     await turnContext.SendActivityAsync(MessageFactory.Text(ThankYou), cancellationToken);
                  }
               }

               // Save the updated dialog state into the conversation state.
               await conversationState.SaveChangesAsync(
                   turnContext, false, cancellationToken);
               break;
         }
      }
   }
}