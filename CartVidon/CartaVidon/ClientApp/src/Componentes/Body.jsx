import Rubros from "../Componentes/Rubros"


import "../Styles/body.css";
import imagenes from "../Assets/imagenes";


const Body = () => {
    return (
        <div className="body_container">


            <table className="tableRbrs">
                <tr>
                    <td><Rubros texto="LOMITOS" img={imagenes.lomoEj} /></td>
                </tr>
                <tr>
                    <td><Rubros texto="PAPAS" img={imagenes.papasEj} /></td>
                </tr>
                <tr>
                    <td><Rubros texto="TOSTADOS" img={imagenes.tostadosEj} /></td>
                </tr>
                <tr>
                    <td><Rubros texto="HAMBUR" img={imagenes.hamburEj} /></td>
                </tr>
                <tr>
                    <td><Rubros texto="LOMITOS" img={imagenes.lomoEj} /></td>
                </tr>
                <tr>
                    <td><Rubros texto="PAPAS" img={imagenes.papasEj} /></td>
                </tr>
                <tr>
                    <td><Rubros texto="TOSTADOS" img={imagenes.tostadosEj} /></td>
                </tr>
                <tr>
                    <td><Rubros texto="HAMBUR" img={imagenes.hamburEj} /></td>
                </tr>
            </table>


        </div>

    )
}

export default Body;