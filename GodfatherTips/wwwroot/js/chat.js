class Message
{
    constructor(username, text, creationDate)
    {
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

document.getElementById('submitButton').addEventListener('click', () =>
{
    var currentdate = new Date();
    creationDate.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });
});

function clearInputField()
{
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage()
{
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;
    
    let creationDate = new Date();
    let message = new Message(username, text);
    sendMessageToHub(message);
}

function addMessageToChat(message)
{
    let isCurrentUserMessage = message.userName === username;

    let containerClass = isCurrentUserMessage ? "container darker bg-primary" : "container bg-light";
    let textAlign = isCurrentUserMessage ? "sender text-right text-white" : "sender text-left";
    let timePosition = isCurrentUserMessage ? "time-right text-light" : "time-left";
    let offset = isCurrentUserMessage ? "col-md-6 offset-md-6" : "";

    let divrow = document.createElement('div');
    divrow.className = "row";

    let divoffset = document.createElement('div');
    divoffset.className = offset;

    let container = document.createElement('div');
    container.className = containerClass;

    let sender = document.createElement('p');
    sender.className = textAlign;
    sender.innerHTML = message.userName;

    let text = document.createElement('p');
    text.innerHTML = message.text;
    text.classList = textAlign;

    let creationDate = document.createElement('span');
    creationDate.className = timePosition;

    var currentdate = new Date();
    creationDate.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });

    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(creationDate);
    divoffset.appendChild(container);
    divrow.appendChild(divoffset);
    chat.appendChild(divrow);
}
