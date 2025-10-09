import React, { createContext, useContext, useState, useEffect } from 'react';

const AuthContext = createContext();

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};

export const AuthProvider = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Check if user is logged in on app start
    const checkAuth = () => {
      try {
        const token = localStorage.getItem('authToken');
        const userData = localStorage.getItem('currentUser');
        const loginTime = localStorage.getItem('loginTime');
        
        if (token && userData && loginTime) {
          // Check if token is expired (24 hours)
          const now = new Date().getTime();
          const loginTimestamp = parseInt(loginTime);
          const twentyFourHours = 24 * 60 * 60 * 1000; // 24 hours in milliseconds
          
          if (now - loginTimestamp < twentyFourHours) {
            setIsAuthenticated(true);
            setUser(JSON.parse(userData));
          } else {
            // Token expired, clear storage
            logout();
          }
        }
      } catch (error) {
        console.error('Error checking authentication:', error);
        logout();
      } finally {
        setLoading(false);
      }
    };

    checkAuth();
  }, []);

  const login = (username, password) => {
    return new Promise((resolve, reject) => {
      // Simulate API call delay
      setTimeout(() => {
        // Simple authentication - in production, this would be a real API call
        if (username === 'you' && password === 'password') {
          const userData = {
            username: username,
            name: 'Test User',
            role: 'admin',
            loginTime: new Date().toISOString()
          };
          
          // Generate a simple token (in production, this would come from server)
          const token = btoa(username + ':' + Date.now());
          
          // Store authentication data
          localStorage.setItem('authToken', token);
          localStorage.setItem('currentUser', JSON.stringify(userData));
          localStorage.setItem('loginTime', Date.now().toString());
          localStorage.setItem('isLoggedIn', 'true');
          
          setIsAuthenticated(true);
          setUser(userData);
          resolve(userData);
        } else {
          reject(new Error('Felaktigt användarnamn eller lösenord'));
        }
      }, 1000); // Simulate network delay
    });
  };

  const logout = () => {
    // Clear all authentication data
    localStorage.removeItem('authToken');
    localStorage.removeItem('currentUser');
    localStorage.removeItem('loginTime');
    localStorage.removeItem('isLoggedIn');
    
    setIsAuthenticated(false);
    setUser(null);
  };

  const value = {
    isAuthenticated,
    user,
    login,
    logout,
    loading
  };

  return (
    <AuthContext.Provider value={value}>
      {children}
    </AuthContext.Provider>
  );
};
