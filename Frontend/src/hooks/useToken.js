import { jwtDecode } from "jwt-decode";
import { useEffect, useState } from "react";

const useToken = () => {
  const key = "token";
  const [token, setToken] = useState(() => localStorage.getItem(key) || "");

  useEffect(() => {
    if (token) {
      localStorage.setItem(key, token);
    } else {
      localStorage.removeItem(key);
    }
  }, [key, token]);

  const saveToken = (newToken) => setToken(newToken);
  const getToken = () => localStorage.getItem(key);
  const removeToken = () => {
    setToken("");
    localStorage.removeItem(key);
  };

  const isTokenValid = () => {
    if (!token) return false;
    try {
      const { exp } = jwtDecode(token);
      return Date.now() < exp * 1000;
    } catch (error) {
      return false;
    }
  };

  return {
    saveToken,
    getToken,
    removeToken,
    isTokenValid,
  };
};

export default useToken;
