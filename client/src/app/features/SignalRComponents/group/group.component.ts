import { Component, OnInit } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { MessagePackHubProtocol } from "@microsoft/signalr-protocol-msgpack";

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {
  // properties

  // create connection
  connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/hubs/color")
    .withHubProtocol(new MessagePackHubProtocol()) // i am request to connect me throgh message protocol
    .build();
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
  // join group 
  joinGroup(groupName: any) {
    this.connection.invoke("JoinGroup", groupName);
  }
  // TriggerGroup 
  triggerGroup(group: any) {
    this.connection.invoke("TriggerGroup", group);
  }
  // client event
  clientEvents = () => {

    this.connection.on("TriggerColor", (color: string) => {
      console.log("color: ", color);
      document.getElementsByTagName('body')[0].style.backgroundColor = color;
    });

    this.connection.on("ColorChange", (color: string) => {
      console.log("color: ", color);
      document.getElementsByTagName('body')[0].style.backgroundColor = color;
    });
  }
  // change color
  changeColor = (color: any) => {

    this.connection.invoke("ColorChange", color);
  }

}
