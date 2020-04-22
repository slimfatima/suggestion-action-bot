// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Microsoft.BotBuilderSamples
{
    // This bot will respond to the user's input with suggested actions.
    // Suggested actions enable your bot to present buttons that the user
    // can tap to provide input. 
    public class SuggestedActionsBot : ActivityHandler
    {
        public const string WelcomeText = @"This bot is here to answer questions you may have about Wbizmanager Solution";
        public const string Intro1 = @"What question do you have for me today?";
        
        public const string Intro4 = @"my name is";
        public const string Intro3= @"Any other question?";


        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            // Send a welcome message to the user and tell them what actions they may perform to use this bot
            await SendWelcomeMessageAsync(turnContext, cancellationToken);
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {

            // Extract the text from the message activity the user sent.
            var text = turnContext.Activity.Text.ToLowerInvariant();

            // Take the input from the user and create the appropriate response.
            var responseText = ProcessInput(text);

            // Respond to the user.
            await turnContext.SendActivityAsync(responseText, cancellationToken: cancellationToken);

      
        }
        private static async Task SendWelcomeMessageAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(
                        $"Welcome to SuggestedActionsBot. {WelcomeText}",
                        cancellationToken: cancellationToken);
                    await SendSuggestedActionsAsync(turnContext, cancellationToken, Intro1);
                }
            }
        }

        private static string ProcessInput(string text)
        {
            switch (text)
            {
                case "why wbizmanager?":
                    {
                        return @"Wbizmanager is an integrated suite of applications that manages business processes such as sales,
                                purchasing, accounting, human resources, customer support, CRM, and inventory developed for SME";
                    }

                case "what can i use wbizmanager for?":
                    {
                        return @"Wbizmanager is an integrated suite of applications that manages business processes such as sales,
                                purchasing, accounting, human resources, customer support, CRM, and inventory developed for SME";
                    }

                case "what does wbizmanager promise?":
                    {
                        return @"Wbizmanager is an integrated suite of applications that manages business processes such as sales,
                            purchasing, accounting, human resources, customer support, CRM, and inventory developed for SME";
                    }

                case "what solution can wbizmanager provide?":
                    {
                        return @"Wbizmanager is an integrated suite of applications that manages business processes such as sales,
                            purchasing, accounting, human resources, customer support, CRM, and inventory developed for SME";
                    }

                case "why should i use wbizmanager?":
                    {
                        return @"Wbizmanager is an integrated suite of applications that manages business processes such as sales,
                            purchasing, accounting, human resources, customer support, CRM, and inventory developed for SME";
                    }
                default:
                    {
                        return text;
                    }
            }
        }

        // Creates and sends an activity with suggested actions to the user. When the user
        /// clicks one of the buttons the text value from the "CardAction" will be
        /// displayed in the channel just as if the user entered the text. There are multiple
        /// "ActionTypes" that may be used for different situations.
        private static async Task SendSuggestedActionsAsync(ITurnContext turnContext, CancellationToken cancellationToken, string introText)
        {
            var reply = MessageFactory.Text(introText);

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction() { Title = "Why Wbizmanager?", Type = ActionTypes.ImBack, Value = "Why Wbizmanager?" },
                    new CardAction() { Title = "What can I use Wbizmanager for?", Type = ActionTypes.ImBack, Value = "What can I use Wbizmanager for?" },
                    new CardAction() { Title = "What does Wbizmanager promise?", Type = ActionTypes.ImBack, Value = "What does Wbizmanager promise?" },
                    new CardAction() { Title = "What solution can Wbizmanager provide?", Type = ActionTypes.ImBack, Value = "What solution can Wbizmanager provide?" },
                    new CardAction() { Title = "Why should I use Wbizmanager?", Type = ActionTypes.ImBack, Value = "Why should I use Wbizmanager?" },
                    new CardAction() {Title = "what do i use it for?",Type = ActionTypes.ImBack, Value = "What should i use it for"},
                    new CardAction() {Title ="Benefits of wbizmanger?",Type = ActionTypes.ImBack, Value = "Benefits of wbizmanager"},
                    new CardAction() {Title ="our promises",Type = ActionTypes.ImBack, Value = "our promises"},
                },
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }
    }
}
