class Message {
    constructor(username, text, creationDate) {
        this.userName = username;
        this.text = text;
        this.creationDate = creationDate;
    }
}

// userName is declared in razor page.
const username = userName;
const textInput = document.getElementById('messageText');
const creationDateInput = document.getElementById('creationDate');
const chat = document.getElementById('chat');
const messagesQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
    var currentdate = new Date();
    creationDate.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;

    let creationDate = new Date();
    let message = new Message(username, text);
    sendMessageToHub(message);
}

function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;

    let container = isCurrentUserMessage ? "msg_container_send" : "msg_container";
    let justify = isCurrentUserMessage ? "d-flex justify-content-end mb-4" : "d-flex justify-content-begin mb-4";
    let time = isCurrentUserMessage ? "msg_time_send" : "msg_time";

    let div1 = document.createElement('div');
    div1.className = justify;

    let div2 = document.createElement('div');
    div2.className = container;

    let senderi = document.createElement('i');
    senderi.style = "color: darkred";
    senderi.innerHTML = message.userName;
    let sender = document.createElement('p');
    sender.innerHTML = message.text;

    let creationDate = document.createElement('span');
    creationDate.className = time;

    var currentdate = new Date();
    creationDate.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });

    div2.appendChild(senderi);
    div2.appendChild(sender);
    div2.appendChild(creationDate);
    div1.appendChild(div2);
    chat.appendChild(div1);
}
