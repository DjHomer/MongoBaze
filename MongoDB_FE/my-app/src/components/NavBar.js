import React from 'react';
import {Link,useNavigate} from "react-router-dom";
import {useState} from 'react';
import './NavBar.css'

function NavBar(){
    //const history=useHistory();
    const navigate=useNavigate();
    const [click,setClick] =useState(false)
    const [button,setButton]=useState(true)
    
    const handleClick=()=>setClick(!click)
    const closeMobileMenu=()=>setClick(false)

   // const handleHistory=()=>{
       // history.push("/")
   // }

   const handleNavigate=()=>{
     navigate.push("/")
   }

    return (
      
            <nav className='navbar'>
              <div className='navbar-container'>

              <div className='menu-icon' onClick={handleClick}>
                <i className={click ? 'fas fa-times' :'fas fa-bars'}/>
              </div>
           
              <ul className={click? 'nav-menu active':'nav-menu'}>
                
                  <li className="nav-item">
                    <Link to="/" className='nav-links' onClick={closeMobileMenu}>
                        Homepage
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link to="/busPreduzeca" className='nav-links' onClick={closeMobileMenu}>
                        Bus Preduzeca
                    </Link>
                  </li>  
                  <li className="nav-item">
                    <Link to="/voznje" className='nav-links' onClick={closeMobileMenu}>
                        Voznje
                    </Link>
                  </li> 
                  
                  <li className="nav-item">
                    <Link to="/proveriRezervaciju" className='nav-links' onClick={closeMobileMenu}>
                     Rezervacija
                    </Link>
                  </li> 
                  
                   
                </ul>

              </div>
               
      </nav>
       
     
        
    )
}

export default NavBar