import { Component, OnInit } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import CustomeRetryPolicy from '../signalR/CustomeRetryPolicy';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {
  // properties
  count: any;
  viewCount: any;
  firstName: any;
  lastName: any;

  // create connection 
  connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    //.configureLogging(new CustomLogger()) // custom logger
    .withUrl("https://localhost:5001/hubs/view", {
      transport:
        signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.ServerSentEvents
    })
    // aslo we can create custome retry policy like
    //.withAutomaticReconnect(new CustomeRetryPolicy())
    .withAutomaticReconnect() // if server down it will try again and again to connect we can also pass array of to wait [0,10,20,30]
    .build();
  // this just tell is to use type connection { transport:  signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.ServerSentEvents}

  constructor() { }

  ngOnInit(): void {
    this.startConnection();
    this.viewCountUpdate();

  }

  // start a connection
  startConnection = () => {
    this.connection.start().then(this.startSuccess, this.startFail);
    // on reconnect\
    this.connection.onreconnected((connectionId) => {
      document.getElementsByTagName('body')[0].style.backgroundColor = "green";
    });
    // on re connecting
    this.connection.onreconnecting((error) => {
      document.getElementsByTagName('body')[0].style.backgroundColor = "yellow";
    });
    // on re close
    this.connection.onclose((error) => {
      document.getElementsByTagName('body')[0].style.backgroundColor = "red";
    });
  }
  // connection success
  startSuccess = () => {
    console.log("Connected.");
    this.notify();
  }
  // connection faild
  startFail = () => {
    console.log("Connection failed.");
  }
  // notify server we'r watching
  notify = () => {
    this.connection.send("NotifyWatching");
  }
  // view update message from a client
  viewCountUpdate = () => {
    this.connection.on("viewCountUpdate", (value: number) => {
      this.count = value;
      console.log("counter ==", this.count)
    });
    // increment view 
    this.connection.on("IncrementView", (value: number) => {
      this.viewCount = value;
      // stop it
      if (value % 10 === 0) this.connection.off("IncrementView");
    });
  }
  // concatenate string
  concatString() {
    // send to the hub
    this.connection.invoke("GetFullName", this.firstName, this.lastName).then((value) => {
      console.log(value);
    }, error => {
      console.log("error", error);
    });
  }
  // update view count 
  incrementViewCount() {
    // send to  hub
    this.connection.invoke("IncrementServerView").catch(err => console.error(err));
  }

}
