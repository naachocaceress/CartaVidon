
import Navbar from "./Componentes/Navbar";
import Bottombar from "./Componentes/Bottombar";
import Body from "./Componentes/Body";

import "./style.css";


const App = () => {


    return (
        <div className="container-fluid">

            <Navbar/>

            <Body/>

            <Bottombar/>
            
        </div>
    )
}

export default App;