import axios from 'axios'

const api =
{
    user : 
    {
        GetPage(pageNumber, perPage)
        {
            return axios.get(`https://localhost:44343/api/users?pageNumber=${pageNumber}&perPage=${perPage}`);
        },

        GetAll()
        {
            return axios.get("https://localhost:44343/api/users/all")
        },

        Delete(id)
        {
            return axios.delete(`https://localhost:44343/api/users/${id}`);
        },

        GetOne(id)
        {
            return axios.get(`https://localhost:44343/api/users/${id}`)
        },

        Create(user)
        {
            return axios.post('https://localhost:44343/api/users/', user);
        },

        Update(user)
        {
            return axios.put('https://localhost:44343/api/users/', user);
        }
    },

    meeting: 
    {
        GetAll()
        {
            return axios.get("https://localhost:44343/api/meetings/")
        },

        Delete(id)
        {
            return axios.delete(`https://localhost:44343/api/meetings/${id}`);
        },

        GetOne(id)
        {
            return axios.get(`https://localhost:44343/api/meetings/${id}`)
        },

        Create(meeting)
        {
            return axios.post('https://localhost:44343/api/meetings/', meeting);
        },

        Update(meeting)
        {
            return axios.put('https://localhost:44343/api/meetings/', meeting);
        }
    }
}

export default api;