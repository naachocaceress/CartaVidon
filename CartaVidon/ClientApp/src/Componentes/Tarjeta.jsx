/* eslint-disable jsx-a11y/anchor-is-valid */
const Tarjeta = ({titulo, parrafo, textobtn, children }) => {
    return (
        <div className="card text-center bg-dark mt-5">
            <div className="card-body">
                <h1 className="card-title text-info">{titulo}</h1>
                <p className="card-text text-light">{parrafo}</p>
                <a href="#" className="btn btn-danger">{textobtn}</a>
                {children}

            </div>
        </div>
    )
}

export default Tarjeta;