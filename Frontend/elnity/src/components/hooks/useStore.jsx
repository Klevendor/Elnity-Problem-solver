import { useContext } from "react";
import {StoreContext} from "../context/StoreContext"

const useStore = () =>{
    return useContext(StoreContext)
}

export default useStore;