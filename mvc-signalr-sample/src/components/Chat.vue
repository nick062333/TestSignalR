<template>
  <div>
    <h1>Chat 1</h1>
    <div>
      輸入訊息: <input type="text" v-model="message" />
        <button @click="sendMessage">Send</button>
    </div>
    <div>
    <div>
        <ul>
            <li v-for="msg in messages" :key="msg">{{ msg }}</li>
        </ul>    
      </div>
  </div>
  </div>
</template>

<script setup lang="ts">
// Import SignalR
// BEGIN: SignalR logic
import * as signalR from "@microsoft/signalr";
import { ref } from "vue";

//import MessagePackHubProtocol
import { MessagePackHubProtocol } from "@microsoft/signalr-protocol-msgpack";

const message = ref("");

//signalr meeagepack實作
const connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7235/chatHub")
  .withHubProtocol(new MessagePackHubProtocol())
  .withAutomaticReconnect()
  // .configureLogging(signalR.LogLevel.Information)
  .build();

start();

connection.onclose(async () => {
    await start();
});


// const message = ref("");
const messages = ref([]);

connection.on("Send", (user, message) => {
  const received = `${user}: ${message}`;
  messages.value.push(received);
});


const sendMessage = () => {
  connection.invoke("Send", message.value, new Date());
  console.log("sendMessage");
};

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
</script>

<style scoped>
/* Add your custom styles here */
</style>
