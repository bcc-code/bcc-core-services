import 'dotenv/config';
import express from 'express';
import swaggerUi from 'swagger-ui-express'
import bodyParser from 'body-parser'
import { createProxyMiddleware } from 'http-proxy-middleware'
import getDocumentationData from './getDocumentationData.js';
import tokenEndpoint from './tokenEndpoint.js';

const app = express()

const urls = getDocumentationData()

var options = {
    explorer: true,
    swaggerOptions: {
      persistAuthorization: true,
      urls
    }
}
const router = new express.Router()
router.post("/token", bodyParser.urlencoded(), tokenEndpoint)
router.use(express.static('public'))

router.use(swaggerUi.serve, swaggerUi.setup(null, options))

app.use("/docs", router)

const port = process.env.PORT || 3001
app.listen(port, () => {
  console.log(`Server is running on port ${port}`)
});

const requestRedirectUrl = process.env.REQUEST_REDIRECT_URL
if(requestRedirectUrl){
  app.use('*', createProxyMiddleware({target: requestRedirectUrl, changeOrigin: true}));
}
