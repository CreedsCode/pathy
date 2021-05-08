import { router } from "./server";
import http from "http";
import config from "./util/config";


const httpServer = http.createServer(router);
httpServer.listen(config.server.port, () => console.log(`Server running on ${config.server.hostname}:${config.server.port}`));
