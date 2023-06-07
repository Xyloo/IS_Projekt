
const PROXY_CONFIG = [
  {
    context: [
      "/api",
   ],
    target: "http://localhost:5000",
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
