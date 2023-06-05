export const AppService = {

    async getApps(axios) {
      const response = await axios.get("/app/get-apps"
      ).catch((err) => { console.log(err.response.data) })
  
      return response.data
    },
  
    async getAppPreview(axios,email,appId) {
        const response = await axios.post("/app/get-app-preview",{
            email:email,
            appId:appId
        },
        ).catch((err) => { console.log(err.response.data) })
    
        return response.data
      },

    async registerApp(axios,email,appId) {
        const response = await axios.post("/app/register-app",{
            email:email,
            appId:appId
        },
        ).catch((err) => { console.log(err.response.data) })
    
        return response.data
      },

    async getUserApps(axios,email) {
        const response = await axios.post("/app/get-user-apps",{
            email:email
        },
        ).catch((err) => { console.log(err.response.data) })
        return response.data
      },
    }