"use strict";
exports.__esModule = true;
var dotenv_1 = require("dotenv");
dotenv_1["default"].config();
var SERVER_HOSTNAME = process.env.SERVER_HOSTNAME || "localhsot";
var SERVER_PORT = process.env.SERVER_PORT || 8080;
var SERVER = {
    hostname: SERVER_HOSTNAME,
    port: SERVER_PORT
};
var config = {
    server: SERVER
};
exports["default"] = config;
