//using CFIssueTrackerCommon.Interfaces;
//using CFIssueTrackerCommon.Models;
//using Microsoft.AspNetCore.SignalR.Client;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CFIssueTrackerCommon.Services
//{
//    public class SystemEventSubscriberService : ISystemEventSubscriberService
//    {
//        private Dictionary<string, Action<AuditEvent>> _actionEventAddedActions = new();

//        public SystemEventSubscriberService()
//        {
//            ConnectHub().Wait();
//        }

//        public string RegisterAuditEventAdded(Action<AuditEvent> action)
//        {
//            var subscribeId = Guid.NewGuid().ToString();
//            _actionEventAddedActions.Add(subscribeId, action);
//            return subscribeId;
//        }

//        public void Unsubscribe(string subscribeId)
//        {
//            if (_actionEventAddedActions.ContainsKey(subscribeId)) _actionEventAddedActions.Remove(subscribeId);
//        }

//        private Task ConnectHub()
//        {
//            var connection = new HubConnectionBuilder()
//               //.WithUrl("https://localhost:44384/addecisiondatahub") //Make sure that the route is the same with your configured route for your HUB
//               //.WithUrl("https://localhost:44314/apinotification") //Make sure that the route is the same with your configured route for your HUB
//               .WithUrl("/systemeventhub")
//               .Build();
            
//            // Set handler for audit event added
//            connection.On<AuditEvent>("ReceiveAuditEventAdded", (auditEvent) =>
//            {
//                System.Diagnostics.Debug.WriteLine("Notification: AuditEvent");
//                foreach(var subscribeId in _actionEventAddedActions.Keys)
//                {
//                    _actionEventAddedActions[subscribeId].Invoke(auditEvent);
//                }
//            });

//            connection.StartAsync().ContinueWith(task =>
//            {
//                if (task.IsFaulted)
//                {
//                    //Do something if the connection failed
//                }
//                else
//                {
//                    //if connection is successfull, do something
//                    //connection.InvokeAsync("sendMessage", myData);
//                    //connection.InvokeAsync("DataChanged", "Campaign");                                       
//                    //connection.InvokeAsync("Test", "This is a test message");
//                }
//            }).Wait();

//            return Task.CompletedTask;
//        }
//    }
//}
