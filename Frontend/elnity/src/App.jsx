import {Route, Routes} from "react-router-dom"
import Login from './components/screens/login/Login'
import Register from "./components/screens/register/Register"
import Landing from "./components/screens/landing/LandingPage"
import RequireAuth from './components/helpers/RequireAuth'
import PersistLogin from './components/helpers/PersistLogin'
import Home from "./components/screens/home/Home"

const App = () => {

  return (
    <Routes>

      {/* private routes */}
      <Route element={<PersistLogin />}>
        <Route element={<RequireAuth allowedRoles={["User"]} />}>
          <Route path="/home/*" element={<Home />} />
        </Route>
      </Route>

       {/* public routes */}

      <Route path="/" element={<Landing/>}/>
      <Route path="/login" element={<Login/>}/>
      <Route path="/register" element={<Register/>}/>
    </Routes>
  )
}


export default App
