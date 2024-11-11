import { useEffect, useState } from "react";
import useFetch from "../hooks/useFetch";
import GlobalContext from "./GlobalContext";
import useToken from "../hooks/useToken";

export default function GlobalContextProvider({ children }) {
  const { data, error, loading, fetchData } = useFetch();
  const [isAuthenticated, setIsAuthenticated] = useState(true);
  let { token, saveToken, removeToken } = useToken();

  async function checkAuthenticated() {
    let request = await fetchData("user/self", "GET", null, true);
    if (request === "error") {
      setIsAuthenticated(false);
      removeToken();
    }
  }

  useEffect(() => {
    checkAuthenticated();
  }, [token]);

  return (
    <GlobalContext.Provider
      value={{
        error,
        loading,
        data,
        fetchData,
        saveToken,
        isAuthenticated,
      }}
    >
      {children}
    </GlobalContext.Provider>
  );
}
