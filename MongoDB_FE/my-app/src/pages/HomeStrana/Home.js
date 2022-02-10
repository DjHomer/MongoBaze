import React from 'react';
import {useNavigate} from "react-router-dom";
import {useState} from 'react';
import { Button } from '../../components/Button'
import './Home.css'


function Home(){
    const navigate=useNavigate();
   
    const [click,setClick] =useState(false)
    const [button,setButton]=useState(true)
    
    const handleClick=()=>setClick(!click)
    const closeMobileMenu=()=>setClick(false)

   // const handleHistory=()=>{
       // history.push("/")
   // }

   const handleNavigate1=()=>{
     navigate("/busPreduzeca")
   }

   const handleNavigate2=()=>{
       navigate('/voznje')
   }

    return (
     
        <div className='homePageContainer'>
         <h1 className="Naslov"> Dobrodošli na naš sajt!  </h1> 
         <h3 className="Naslov1">Kreirajte nezaboravne uspomene sa nama!</h3>
         <div className='buttonContainer'>
               <Button
                className='btns hover-zoom'
                buttonStyle='btn--primary'
                buttonSize='btn--large'
                onClick= {handleNavigate1}
                >
                 Pogledajte listu dostupnih prevoznika
                </Button>
                <Button
                className='btns'
                buttonStyle='btn--primary'
                buttonSize='btn--large'
                onClick={handleNavigate2}
                >
                 Pogledajte listu dostupnih voznji
                </Button>
            </div>

        </div>
    )
}

export default Home