// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with EmptyBot .NET Template version v4.7.0

using Microsoft.Bot.Builder.Dialogs;

namespace HCRDialogs
{
   internal class GreetingDialog : ComponentDialog
   {
      public GreetingDialog()
        
      {
         var waterfallSteps = new WaterfallStep[]
        {
    
        };
         //Register the waterfall dialog. 
         AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
         //Register text prompts, so we can re-use them. 
         AddDialog(new TextPrompt(nameof(TextPrompt)));
         //Make sure we start with the waterfall dialog
         InitialDialogId = nameof(WaterfallDialog);

      }
   }
}