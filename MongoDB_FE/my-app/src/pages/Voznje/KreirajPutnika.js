import React, {useState} from 'react'
import Api from '../../services/Api.js'
import Spinner from '../../components/Spinner.js';
import { useNavigate } from "react-router-dom";
import { ImagePicker,FilePicker } from 'react-file-picker'
import {useParams} from "react-router-dom";
import ReactFileReader from 'react-file-reader';
import './KreirajPutnika.css'

function KreirajPutnika(){
    const {id}=useParams();
    const {data:v, loading:load1, error:err1}=Api("Voznja/VratiVoznju/"+id);
    let navigate = useNavigate();




    const [passportBase64,setPassportBase64]=useState("");
    const [fullPassportBase64,setFullPassportBase64]=useState("");
    const [jmbg,setJmbg]=useState("");
    const [email,setEmail]=useState("");
    const [ime,setIme]=useState("");
    const [prezime,setPrezime]=useState("");
    const [prtljagExists,setPrtljagExists]=useState("");
    const [brojPrtljaga,setBrojPrtljaga]=useState(0);
    const [pcrTestFileName,setPcrTestFileName]=useState();
    const [pcrTestBase64,setPcrTestBase64]=useState("");
    const [brojTelefona, setBrojTelefona]=useState("");
    const [showSpinner,setShowSpinner]=useState(false);

    //console.log(id);

    if(err1) throw err1;
    if(load1) return <Spinner/>;

    console.log(v);

    return(
        <div className="parent">
            <form>
                <ul className="form-style-1">
                    <li><label style={{color:"#FFFFFF", fontFamily:"verdana"}}>Ime i prezime <span className="required">*</span></label><input type="text" name="field1" className="field-divided" placeholder="Ime" onChange={(event)=>setIme(event.currentTarget.value)} /> <input type="text" name="field2" class="field-divided" placeholder="Prezime"  onChange={(event)=>setPrezime(event.currentTarget.value)}/></li>
                    <li>
                        <label style={{color:"#FFFFFF", fontFamily:"verdana"}}>Email <span className="required">*</span></label>
                        <input type="email" name="field3" className="field-long" onChange={(event)=>setEmail(event.currentTarget.value)} />
                    </li>
                    <li>
                        <label style={{color:"#FFFFFF", fontFamily:"verdana"}}>JMBG <span className="required">*</span></label>
                        <input type="text" name="field4" className="field-long" onChange={(event)=>setJmbg(event.currentTarget.value)} />
                    </li>
                    <li>
                        <label style={{color:"#FFFFFF", fontFamily:"verdana"}}>Broj Telefona <span className="required">*</span></label>
                        <input type="text" name="field7" className="field-long" onChange={(event)=>setBrojTelefona(event.currentTarget.value)} />
                    </li>
                    <li>
                        <ImagePicker
                            extensions={['jpg', 'jpeg', 'png']}
                            maxSize={5}
                            dims={{minWidth: 100, maxWidth: 2200, minHeight: 100, maxHeight: 2200}}
                            onChange={base64 => {setPassportBase64(base64.slice(23,base64.length));setFullPassportBase64(base64);}}
                            onError={errMsg => alert(errMsg)}
                        >
                            <button class="btn btn-light" type="button" style={{color:"#FFFFFF", fontFamily:"verdana"}}>
                                Pronadji sliku
                            </button>
                        </ImagePicker>
                        <img src={fullPassportBase64} style={{"width":"250px","height":"250px"}}></img>
                        {(passportBase64=="")&&<label style={{"color":"red"}}>*Obavezna slika</label>}<br/>
                        <br/><br/>
                    </li>

                    <li>
                    <label style={{color:"#FFFFFF", fontFamily:"verdana" }} for="field4"><span>Imate prtljag?</span><select name="field4" class="select-field" onChange={(event)=>setPrtljagExists(event.target.value)}>
                    <option value="" >Ne</option>
                    <option value="true" >Da</option>

                    </select ></label>
                    </li>
                    

                    {(prtljagExists !== "") && 
                    <li>

                        <label style={{color:"#FFFFFF", fontFamily:"verdana" }}>Broj Prtljaga <span className="required">*</span></label>
                        <input type="number" name="field5" className="field-divided" onChange={(event)=>setBrojPrtljaga(event.currentTarget.value)}/>
                    </li>}
                    {(v.tipVoznje === "medjunarodni") &&
                    <div class="float-child" style={{width:"100%"}}>
                        <label style={{color:"#FFFFFF"}}>PCR test:</label>
                            <ReactFileReader fileTypes={[".pdf",".docx"],".png",".jpg",".jpeg"} base64={true} multipleFiles={false} handleFiles={(files)=>
                                {
                                    setPcrTestFileName(files.fileList[0].name);
                                    let indOfComma=files.base64.indexOf(",");
                                    setPcrTestBase64(files.base64.slice(indOfComma+1,files.base64.length));
                                }}>
                               <button className='btn' style={{backgroundColor:"#4e9af1", color:"#FFFFFF"}} type="button">Pronadji fajl</button>
                            </ReactFileReader> <label style={{color:"#FFFFFF", fontFamily:"verdana"}}>{pcrTestFileName}</label>
                          {(pcrTestBase64==="")&&<label style={{"color":"red"}}>*Obavezno polje</label>}<br/>
                          <br/><br/>
                    </div>
                    }
                   
                    <li>
                        <input type="button" onClick={()=>KreirajPutnikaIRezervaciju()} value="Rezervisi" />
                    </li>
                   
                </ul>
            </form>
        </div>

    )

    function KreirajPutnikaIRezervaciju() {
        let numPrtljaga = 0
        let prtljagPostoji = false
        setShowSpinner(true);
        if (prtljagExists === "true"){
            numPrtljaga = brojPrtljaga;
            prtljagPostoji = true
        }
        fetch("https://localhost:44335/Putnik/KreirajPutnika",{
          method:"POST",
          headers:{"Content-Type":"application/json"},
          body: JSON.stringify({
              "jmbg":jmbg,
              "ime":ime,
              "prezime":prezime,
              "email":email,
              "broj_Telefona":brojTelefona
            })
        }).then(p=>{
            if(p.ok){
              p.json().then(data=>{
                console.log(data)
                fetch("https://localhost:44335/Rezervacija/KreirajRezervaciju",{
                  method:"POST",
                  headers:{"Content-Type":"application/json"},
                  body:JSON.stringify({
                      "brSedista":Math.round(Math.random()*v.brojPreostalihSedista),
                      "legitimacija":passportBase64,
                      "covid19Test":pcrTestBase64,
                      "status":"na cekanju",
                      "putnik":data,
                      "voznja":id,
                      "cena":v.cenaVoznje,
                      "kolicinaPrt":numPrtljaga,
                      "postojiPrt":prtljagPostoji
                    })
                }).then(p=>{
                    if(p.ok)
                    {
                        p.json().then(data2=>{
                            window.location.replace("/usluge/"+data2)
                            //navigate(`/usluge/${data}`)
                        });
                    }
                }).catch(exc=>{
                  console.log("EXC"+exc);
                })
              });
            }
        }).catch(exc=>{
          console.log(exc);
          setShowSpinner(false);
        })
        setShowSpinner(false);

      

    }
}

export default KreirajPutnika