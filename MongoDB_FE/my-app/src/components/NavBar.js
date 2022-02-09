import React from 'react';
import {Link} from "react-router-dom";

function NavBar(){
    return (
        <div>
            <nav class="navbar navbar-light bg-light">
        <ul>
          <li class="navbar-brand">
            <Link to="/">
                Homepage
            </Link>
          </li>
          <li class="navbar-brand">
            <Link to="/busPreduzeca">
                Bus Preduzeca
            </Link>
          </li>  
          <li class="navbar-brand">
            <Link to="/voznje">
                Voznje
            </Link>
          </li> 
          
          <li class="navbar-brand">
            <Link to="/proveriRezervaciju">
                Proveri rezervaciju
            </Link>
          </li>        
        </ul>
      </nav>
        </div>
    )
}

export default NavBar