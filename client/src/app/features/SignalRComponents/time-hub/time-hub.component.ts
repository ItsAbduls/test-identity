import { Component, OnInit } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import CustomeRetryPolicy from '../signalR/CustomeRetryPolicy';

@Component({
  selector: 'app-time-hub',
  templateUrl: './time-hub.component.html',
  styleUrls: ['./time-hub.component.scss']
})
export class TimeHubComponent implements OnInit {

  // properties
  time: any;


  // create connection 
  connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    //.configureLogging(new CustomLogger()) // custom logger
    .withUrl("https://localhost:5001/hubs/timehub", {
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
    this.timeUpdate();

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
  timeUpdate = () => {
    this.connection.on("updateCurrentTime", (value: any) => {
      this.time = value;
    });

  }
  // change color
  changeColor = (color: any) => {

    this.connection.invoke("ColorChange", color);
  }

}
