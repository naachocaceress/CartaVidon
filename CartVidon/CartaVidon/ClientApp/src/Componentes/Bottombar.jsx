import React, { useState } from 'react';
import "../Styles/bottombar.css";


const Bottombar = () => {
    const [activeItem, setActiveItem] = useState(0);

    const handleItemClick = (index) => {
        setActiveItem(index);
    };

    return (
        <div className="bottombar_container">
            <div className="navigation">
                <ul>
                    <li className={`list ${activeItem === 0 ? 'active' : ''}`} onClick={() => handleItemClick(0)}>
                        <a href="#" className="aBottombar">
                            <span className="icon">
                                <svg xmlns="http://www.w3.org/2000/svg" class="ionicon" viewBox="0 0 512 512"><path fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32" d="M176 464h160M256 464V336M384 224c0-50.64-.08-134.63-.12-160a16 16 0 00-16-16l-223.79.26a16 16 0 00-16 15.95c0 30.58-.13 129.17-.13 159.79 0 64.28 83 112 128 112S384 288.28 384 224z" /><path d="M128 96H48v16c0 55.22 33.55 112 80 112M384 96h80v16c0 55.22-33.55 112-80 112" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32" /></svg>                            </span>
                            <span className="text">Promos</span>
                        </a>
                    </li>
                    <li className={`list ${activeItem === 1 ? 'active' : ''}`} onClick={() => handleItemClick(1)}>
                        <a href="#" className="aBottombar">
                            <span className="icon">
                                <svg xmlns="http://www.w3.org/2000/svg" class="ionicon" viewBox="0 0 512 512"><path d="M322 416c0 35.35-20.65 64-56 64H134c-35.35 0-56-28.65-56-64M336 336c17.67 0 32 17.91 32 40h0c0 22.09-14.33 40-32 40H64c-17.67 0-32-17.91-32-40h0c0-22.09 14.33-40 32-40" fill="none" stroke="currentColor" stroke-linecap="round" stroke-miterlimit="10" stroke-width="32" /><path d="M344 336H179.31a8 8 0 00-5.65 2.34l-26.83 26.83a4 4 0 01-5.66 0l-26.83-26.83a8 8 0 00-5.65-2.34H56a24 24 0 01-24-24h0a24 24 0 0124-24h288a24 24 0 0124 24h0a24 24 0 01-24 24zM64 276v-.22c0-55 45-83.78 100-83.78h72c55 0 100 29 100 84v-.22M241 112l7.44 63.97" fill="none" stroke="currentColor" stroke-linecap="round" stroke-miterlimit="10" stroke-width="32" /><path d="M256 480h139.31a32 32 0 0031.91-29.61L463 112" fill="none" stroke="currentColor" stroke-linecap="round" stroke-miterlimit="10" stroke-width="32" /><path fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32" d="M368 112l16-64 47-16" /><path fill="none" stroke="currentColor" stroke-linecap="round" stroke-miterlimit="10" stroke-width="32" d="M224 112h256" /></svg>                            </span>
                            <span className="text">Menu</span>
                        </a>
                    </li>
                    <li className={`list ${activeItem === 2 ? 'active' : ''}`} onClick={() => handleItemClick(2)}>
                        <a href="#" className="aBottombar">
                            <span className="icon">
                                <svg xmlns="http://www.w3.org/2000/svg" class="ionicon" viewBox="0 0 512 512"><path d="M352 200v240a40.12 40.12 0 01-40 40H136a40.12 40.12 0 01-40-40V224" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32" /><path d="M352 224h40a56.16 56.16 0 0156 56v80a56.16 56.16 0 01-56 56h-40" fill="none" stroke="currentColor" stroke-linecap="round" stroke-miterlimit="10" stroke-width="32" /><path fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32" d="M224 256v160M288 256v160M160 256v160M320 112a48 48 0 010 96c-13.25 0-29.31-7.31-38-16H160c-8 22-27 32-48 32a48 48 0 010-96 47.91 47.91 0 0126 9" /><path d="M91.86 132.43a40 40 0 1160.46-52S160 91 160 96M145.83 64.71C163.22 44.89 187.57 32 216 32c52.38 0 94 42.84 94 95.21a95 95 0 01-1.67 17.79" fill="none" stroke="currentColor" stroke-linecap="round" stroke-miterlimit="10" stroke-width="32" /></svg>                            </span>
                            <span className="text">Tragos</span>
                        </a>
                    </li>
                    <li className={`list ${activeItem === 3 ? 'active' : ''}`} onClick={() => handleItemClick(3)}>
                        <a href="#" className="aBottombar">
                            <span className="icon">
                                <svg xmlns="http://www.w3.org/2000/svg" class="ionicon" viewBox="0 0 512 512"><path d="M398.57 80H113.43v16S87.51 272 256 272 398.57 96 398.57 96zM256 272v160" fill="none" stroke="currentColor" stroke-linejoin="round" stroke-width="32" /><path fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32" d="M352 432H160" /><path fill="none" stroke="currentColor" stroke-linejoin="round" stroke-width="32" d="M112 160h288" /></svg>                            </span>
                            <span className="text">Vinos</span>
                        </a>
                    </li>
                    <li className={`list ${activeItem === 4 ? 'active' : ''}`} onClick={() => handleItemClick(4)}>
                        <a href="#" className="aBottombar">
                            <span className="icon">
                                <svg xmlns="http://www.w3.org/2000/svg" class="ionicon" viewBox="0 0 512 512"><path fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32" d="M352 256l-96 224-62-145" /><path d="M299.42 223.48C291.74 239.75 275.18 252 256 252c-13.1 0-27-5-33.63-9.76C216.27 237.87 208 240 208 250v62a24.07 24.07 0 01-24 24h0a24.07 24.07 0 01-24-24v-56h-2c-35.35 0-62-28.65-62-64a64 64 0 0164-64h8v-8a88 88 0 01176 0v8h8a64 64 0 010 128c-21.78 0-42-13-52.59-32.51z" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32" /></svg> </span>
                            <span className="text">Postres</span>
                        </a>
                    </li>
                    <div className="indicator"></div>
                </ul>

            </div>


        </div>
    )
}

export default Bottombar;
