export const UserService = {

  async getUserInfo(axios, email) {
    const response = await axios.post("/auth/get-user-info", {
      email: email,
    },
    ).catch((err) => { console.log(err.response.data) })

    return response.data
  },

    async changeUserInfo(axios,sendData){
      const formData = new FormData();
      formData.append("username", sendData.username);
      formData.append("fullname", sendData.fullname);
      formData.append("number", sendData.number);
      formData.append("birthday", sendData.birthday);
      formData.append("email", sendData.email);
      formData.append("image", sendData.image);

      const response = await axios.post("/auth/change-user-info", formData).catch((err) =>{
            return false
          })

      return response.data
   },
    
    
  }