import { useState } from "react";
import useToken from "./useToken";

const useFetch = () => {
  const [data, setData] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);
  const {
    saveToken,
    getToken,
    removeToken,
    isTokenValid: isTokenPresent,
  } = useToken();

  const url = "https://localhost:7291/api/v1/";

  async function fetchData(endpoint, method = "GET", body = null, noTokenUse = false, isBlob = false) {
    if (!noTokenUse && !ensureTokenPresent()) return;
  
    let responseData;
    try {
      setLoading(true);
  
      const options = {
        method: method,
        headers: {
          ...(getToken() ? { Authorization: `Bearer ${getToken()}` } : {}),
        },
      };
  
      if (body) {
        if (body instanceof FormData) {
          options.body = body;
        } else {
          options.headers["Content-Type"] = "application/json";
          options.body = JSON.stringify(body);
        }
      }
  
      const response = await fetch(url + endpoint, options);
  
      if (!response.ok) {
        setError("error");
        return "error";
      }
  
      if (isBlob) {
        responseData = await response.blob();
      } else {
        responseData = await response.json();
      }
  
      setData(responseData);
      return responseData;
    } catch (e) {
      setError(e);
      return "error";
    } finally {
      setLoading(false);
    }
  }
  

  function ensureTokenPresent() {
    if (isTokenPresent()) return true;
    removeToken();
    return false;
  }

  return { data, error, loading, fetchData };
};

export default useFetch;
