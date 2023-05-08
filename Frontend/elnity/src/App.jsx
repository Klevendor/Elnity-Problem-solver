import {Route, Routes} from "react-router-dom"
import Login from './components/screens/login/login'
import Main from './components/screens/main/main'
import Register  from './components/screens/register/Register'

function App() {

  return (
    <Routes>
      <Route path="/" element={<Main/>}/>
      <Route path="/login" element={<Login/>}/>
      <Route path="/register" element={<Register/>}/>
    </Routes>
  )
}


export default App
