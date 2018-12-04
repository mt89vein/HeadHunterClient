import axios from 'axios'
import qs from 'qs'

const HTTP = axios.create({
  baseURL: process.env.VUE_APP_BASE_URL,
  headers: {
    Accept: 'application/json',
    ContentType: 'application/json',
  },
})

HTTP.defaults.paramsSerializer = params => {
  return qs.stringify(params, { arrayFormat: 'repeat' })
}

export { HTTP }
