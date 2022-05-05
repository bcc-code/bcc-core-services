import axios from 'axios'

export default async (req, res) => {
    try {
        const scope = req.body.scope;
        const authentication = req.header('Authorization')
        const encoded = authentication.split(' ')[1]
        const decoded = Buffer.from(encoded, 'base64').toString('ascii')
        const [clientId, clientSecret] = decoded.split(':')
        const body = {
            client_id: clientId,
            client_secret: clientSecret,
            grant_type: 'client_credentials',
            audience: process.env.AUTH0_AUDIENCE,
            scope
        }
        const creds = await axios.post(`https://${process.env.AUTH0_ISSUER}/oauth/token`, body)
        res.send(creds.data)
    
    } catch (err) {
        console.log(err.code)
        res.status(400).send("Couldn't get token")
    }
}
