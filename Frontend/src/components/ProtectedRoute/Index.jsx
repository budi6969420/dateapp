import { Navigate } from "react-router-dom";
import useGlobalContext from "../../hooks/useGlobalContext";
import Navbar from "../navbar/Index";

const ProtectedRoute = ({ children }) => {
  let { isAuthenticated } = useGlobalContext();
  return isAuthenticated ? (
    <div>
      <Navbar />
      {children}
    </div>
  ) : (
    <Navigate to="/login" />
  );
};

export default ProtectedRoute;
