import React, {useState} from 'react'
import Api from '../../services/Api.js'
import Spinner from '../../components/Spinner.js';
import { ImagePicker,FilePicker } from 'react-file-picker'
import {useParams} from "react-router-dom";
//import ReactFileReader from 'react-file-reader';
import './KreirajPutnika.css'

function KreirajPutnika(){
    const {id}=useParams();
    const {data:v, loading:load1, error:err1}=Api("Voznja/VratiVoznju/"+id);




    const [passportBase64,setPassportBase64]=useState("");
    const [fullPassportBase64,setFullPassportBase64]=useState("");
    const [jmbg,setJmbg]=useState("");
    const [ime,setIme]=useState("");
    const [prezime,setPrezime]=useState("");
    //const [pcrTestFileName,setPcrTestFileName]=useState();
    //const [pcrTestBase64,setPcrTestBase64]=useState("");
    const [showSpinner,setShowSpinner]=useState(false);

    console.log(id);

    if(err1) throw err1;
    if(load1) return <Spinner/>;

    console.log(v);

    return(
        <div>
            <form>
                <ul className="form-style-1">
                    <li><label>Ime i prezime <span className="required">*</span></label><input type="text" name="field1" className="field-divided" placeholder="Ime" onChange={(event)=>setPrezime(event.currentTarget.value)} /> <input type="text" name="field2" class="field-divided" placeholder="Prezime"  onChange={(event)=>setIme(event.currentTarget.value)}/></li>
                    <li>
                        <label>Email <span className="required">*</span></label>
                        <input type="email" name="field3" className="field-long" />
                    </li>
                    <li>
                        <label>JMBG <span className="required">*</span></label>
                        <input type="text" name="field4" className="field-long" onChange={(event)=>setJmbg(event.currentTarget.value)} />
                    </li>
                    <li>
                        <ImagePicker
                            extensions={['jpg', 'jpeg', 'png']}
                            maxSize={5}
                            dims={{minWidth: 100, maxWidth: 2200, minHeight: 100, maxHeight: 2200}}
                            onChange={base64 => {setPassportBase64(base64.slice(23,base64.length));setFullPassportBase64(base64);}}
                            onError={errMsg => alert(errMsg)}
                        >
                            <button class="btn btn-light">
                                Pronadji sliku
                            </button>
                        </ImagePicker>
                        <img src={fullPassportBase64} style={{"width":"250px","height":"250px"}}></img>
                        {(passportBase64=="")&&<label style={{"color":"red"}}>*Obavezno polje</label>}<br/>
                        <br/><br/>
                    </li>
                   
                    <li>
                        <input type="submit" value="Rezervisi" />
                    </li>
                   
                </ul>
            </form>
        </div>

    )
}

export default KreirajPutnika