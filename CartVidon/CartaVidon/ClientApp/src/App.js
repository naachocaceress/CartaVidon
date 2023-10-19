
import Navbar from "./Componentes/Navbar";
import Bottombar from "./Componentes/Bottombar";
import Body from "./Componentes/Body";

import "./style.css";
import imagenes from "./Assets/imagenes";



const App = () => {


    return (
        <div className="container-fluid">

            <div className="divLogo">
                <img src={imagenes.logo2} alt='Logo central' className='img-circulo' />
            </div>

            <Navbar/>

            <Body/>

            <Bottombar/>
            
        </div>
    )
}

export default App;