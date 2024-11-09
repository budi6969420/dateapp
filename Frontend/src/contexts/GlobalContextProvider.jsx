import { useEffect, useState } from "react";
import useFetch from "../hooks/useFetch";
import GlobalContext from "./GlobalContext";

export default function GlobalContextProvider({ children }) {
  const { data, error, loading, fetchData } = useFetch();

  useEffect(() => {}, []);

  return (
    <GlobalContext.Provider
      value={{
        error,
        loading,
        data,
        fetchData,
      }}
    >
      {children}
    </GlobalContext.Provider>
  );
}
