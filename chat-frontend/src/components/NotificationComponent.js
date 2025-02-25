import React, { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";

const NotificationComponent = () => {
    const [notification, setNotification] = useState("");

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5113/notificationHub", {
                withCredentials: true,
                transport: signalR.HttpTransportType.WebSockets
            })
            .withAutomaticReconnect()
            .build();

        async function startConnection() {
            try {
                await connection.start();
                console.log("Connected to SignalR hub");
                
                connection.on("ReceiveNotification", (message) => {
                    console.log("New Notification:", message);
                    setNotification(message);
                });
            } catch (error) {
                console.error("SignalR Connection Error:", error);
                setTimeout(startConnection, 3000);
            }
        }

        startConnection();

        return () => {
            connection.stop();
        };
    }, []);

    return (
        <div className="container">
            <h4>Real-Time Notifications</h4>
            {notification ? <p>{notification}</p> : <p>No new notifications.</p>}
        </div>
    );
};

export default NotificationComponent;