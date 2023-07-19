import { Component, OnInit } from '@angular/core';
import * as signalR from "@microsoft/signalr";

@Component({
  selector: 'app-vote',
  templateUrl: './vote.component.html',
  styleUrls: ['./vote.component.css']
})
export class VoteComponent implements OnInit {
  // properties
  votes: any;
  // create connection
  connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/hubs/vote").build();
  constructor() { }

  ngOnInit(): void {
    this.startConnection();
    this.clientEvents();
  }
  // start a connection
  startConnection = () => {
    this.connection.start().then(this.startSuccess, this.startFail);
  }
  // connection success
  startSuccess = () => {
    console.log("Connected.");
  }
  // connection faild
  startFail = () => {
    console.log("Connection failed.");
  }
  // client event
  clientEvents = () => {

    this.connection.on("updateVotes", (votes: any) => {
      console.log("votes: ", votes);
      this.votes = votes
    });
  }

}
