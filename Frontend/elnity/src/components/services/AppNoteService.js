export const AppNoteService = {

    async getNotesUser(axios, email) {
      const response = await axios.post("/note/get-user-notes", {
        email: email,
      },
      ).catch((err) => { console.log(err.response.data) })
  
      return response.data
    },

    async deleteNote(axios, id) {
        const response = await axios.post("/note/delete-note", {
          id: id,
        },
        ).catch((err) => { console.log(err.response.data) })
    
        return response.data
      },

    async addNote(axios,sendData){
        const formData = new FormData();
        formData.append("email", sendData.email);
        formData.append("name", sendData.name);
        formData.append("status", sendData.status);
        formData.append("currentState", sendData.current);
        formData.append("note", sendData.note);
        formData.append("image", sendData.image);
  
        const response = await axios.post("/note/add-note", formData).catch((err) =>{
              return false
            })
  
        return response.data
     },
      
      
    }