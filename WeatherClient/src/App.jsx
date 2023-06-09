import { useState, useEffect } from "react";
import axios from "axios";
import "./App.css";
import "mdb-react-ui-kit/dist/css/mdb.min.css";
import "./index.css";
import "./App.css";
import {
  MDBCard,
  MDBCardBody,
  MDBCol,
  MDBContainer,
  MDBRow,
  MDBTypography,
} from "mdb-react-ui-kit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faWind,
  faDroplet /*faSun */,
} from "@fortawesome/free-solid-svg-icons";

function WeatherCall() {
  const [data, setData] = useState({});
  const [time, setTime] = useState({});

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [weatherResponse, timeResponse] = await Promise.all([
          axios.get("http://dev.kjeld.io:20300/weather/stockholm"),
          axios.get("http://dev.kjeld.io:20300/time"),
        ]);

        setData(weatherResponse.data);
        console.log(timeResponse.data);
        setTime(timeResponse.data);
      } catch (error) {
        console.log(error);
      }
    };

    fetchData();
  }, []);

  return (
    <section className="vh-100">
      <MDBContainer className="h-100">
      <MDBRow className="justify-content-center align-items-center h-100">
          <MDBCol md="8" lg="6" xl="4">
            <MDBCard style={{ color: "#4B515D", borderRadius: "50px" }}>
              <MDBCardBody className="p-4">
              <div className="d-flex">
                  <MDBTypography
                    tag="h6"
                    className="flex-grow-1"
                    style={{ fontSize: "20px", fontWeight: "bold"}}

                  >
                    {data.city}
                  </MDBTypography>
                  <MDBTypography tag="h6" style={{ fontSize: "20px", fontWeight: "bold" }}>
                    {time.hour}:{time.minute}
                  </MDBTypography>
                </div> 

                <div className="d-flex flex-column text-center mt-5 mb-4">
                  <MDBTypography
                    tag="h6"
                    className="display-3 mb-0 font-weight-bold"
                    style={{ color: "#1C2331" }}
                  >
                    {data.temperature}°C
                  </MDBTypography>
                  <span className="small" style={{ color: "#868B94" }}>
                    {data.weather}
                  </span>
                  <span className="small" style={{ color: "#868B94", fontWeight: "bold" }}>
                    Sunny 
                  </span>
                </div>

                <div className="d-flex justify-content-start align-items-center">
                <div className="d-flex flex-column text-center" style={{ marginTop: "20px" }}>
  <FontAwesomeIcon icon={faWind} className="wind-animation" />
  <span className="ms-1"> {data.wind} km/h </span>
</div>
<div className="ml-auto">
  <img
    src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-weather/ilu3.webp"
    width="115px"
    alt="weather"
    className="image-animation"
    style={{ marginRight: "15px" }}
  />
</div>

  <div className="d-flex flex-column text-center mt-5 mb-4">
  <FontAwesomeIcon
    icon={faDroplet}
    className="droplet-animation"
  />
  <span className="ms-1"> {data.humidity}%</span>
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

export default WeatherCall;
