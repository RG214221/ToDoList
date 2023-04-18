import axios from 'axios';
axios.defaults.baseURL = process.env.REACT_APP_BaseUrl;
axios.interceptors.response.use(
  function (response) {
    return response;
  },
  function (error) {
    console.log(error);
    return Promise.reject("Error in api call: " + error);
  }
);


export default {
  getTasks: async () => {
    const result = await axios.get('')
    return result.data;
  },

  addTask: async (name) => {

    console.log('addTask', name)
    const result = await axios.post('', { Name: name, IsComplete: false })
    return result.data;
  },

  setCompleted: async (id, name, isComplete) => {

    console.log('setCompleted', { id, isComplete })
    const result = await axios.put('', { Id: id, Name: name, IsComplete: isComplete })
    return result.data;
  },

  deleteTask: async (id) => {
    console.log('deleteTask')
    const result = await axios.delete(`/?id=${id}`)
  }
};
