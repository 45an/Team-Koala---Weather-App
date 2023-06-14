import React from "react";
import {
  MDBCard,
  MDBCardBody,
  MDBCol,
  MDBContainer,
  MDBIcon,
  MDBRow,
  MDBTypography,
} from "mdb-react-ui-kit";
import "./App.css";
import "mdb-react-ui-kit/dist/css/mdb.min.css";
import "./index.css"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faWind } from "@fortawesome/free-solid-svg-icons";
import { faDroplet } from "@fortawesome/free-solid-svg-icons";
import { faSun } from "@fortawesome/free-solid-svg-icons";


export default function Basic() {
  return (
    <section className="vh-100" >
      <MDBContainer className="h-100">
        <MDBRow className="justify-content-center align-items-center h-100">
          <MDBCol md="8" lg="6" xl="4" >
            <MDBCard style={{ color: "black", borderRadius: "35px"}}
            >
              <MDBCardBody className="p-4">
                <div className="d-flex">
                  <MDBTypography tag="h6" className="flex-grow-1" style={{fontSize: '23px'}}>
                   Stockholm
                  </MDBTypography>
                  
                </div>

                <div className="d-flex flex-column text-center mt-5 mb-4">
                  <MDBTypography
                    tag="h6"
                    className="display-3 mb-0 font-weight-bold"
                    style={{ color: "#1C2331" }}
                  >
                    {" "}
                    23°C{" "}
                  </MDBTypography>
                  <span className="small" style={{ color: "#868B94" }}>
                    Sunny
                  </span>
                </div>

                <div className="d-flex align-items-space between">
                  <div className="flex-grow-1" style={{fontSize: '1rem'}}>
                    <div>
                    <FontAwesomeIcon icon={faWind}  />
                      <span className="ms-1"> 2 km/h</span>
                    </div>
                    <div>
                    <FontAwesomeIcon icon={faDroplet}/>
                      <span className="ms-1"> 40% </span>
                    </div>
                    <div>
                    <FontAwesomeIcon icon={faSun}/>
                      <span className="ms-1"> 0.2h </span>
                    </div>
                  </div>
                  <div>
                    <img
                      src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-weather/ilu3.webp"
                      width="100px"
                    />
                  </div>
                </div>
              </MDBCardBody>
            </MDBCard>
          </MDBCol>
        </MDBRow>
      </MDBContainer>
    </section>
  );
}