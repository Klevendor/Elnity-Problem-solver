export const SectionService = {

    async getCourseSectionsWithData(axios, courseId) {
      const response = await axios.post("/sections/get-all-course-data", {
        courseId: courseId,
      },
      ).catch((err) => { console.log(err.response.data) })
  
      return response.data
    },
  
    async changeSectionName(axios, sectionId, newTitle) {
      const response = await axios.post("/sections/change-section-name", {
        sectionId: sectionId,
        newTitle: newTitle
      },
      ).catch((err) => { console.log(err.response.data) })
  
      return response.data
    },
  
    async createSection(axios, courseId, title) {
      const response = await axios.post("/sections/add-section", {
        courseId: courseId,
        title: title
      },
      ).catch((err) => { console.log(err.response.data) })
  
      return response.data
    },
  
    async deleteSection(axios, courseId, sectionId) {
      const response = await axios.delete("/sections/delete-section", {data:{
        courseId: courseId,
        sectionId: sectionId
      }},
      ).catch((err) => { console.log(err.response.data) })
  
      return response.data
    }
  }