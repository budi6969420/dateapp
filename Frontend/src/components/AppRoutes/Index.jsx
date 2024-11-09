import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

const AppRoutes = () => (
  <Router>
    <Routes>
      <Route path="/" />
      <Route path="/about" />
      <Route path="/contact" />
    </Routes>
  </Router>
);

export default AppRoutes;
