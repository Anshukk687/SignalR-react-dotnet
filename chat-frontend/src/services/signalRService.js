import * as signalR from '@microsoft/signalr';

const connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5000/notificationHub')
    .build();

export const startSignalR = async () => {
    try {
        await connection.start();
        console.log('SignalR Connected');
    } catch (error) {
        console.error('Connection error:', error);
    }
};

export const subscribeToNotifications = (callback) => {
    connection.on('ReceiveNotification', callback);
};