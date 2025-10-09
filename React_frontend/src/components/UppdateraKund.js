import React, { useState, useEffect } from 'react';
import {
  Container,
  Paper,
  Typography,
  TextField,
  Button,
  Box,
  Alert,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  CircularProgress
} from '@mui/material';
import Header from './Header';
import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import { customerService } from '../services/api';

const UppdateraKund = () => {
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [loading, setLoading] = useState(false);
  const [customers, setCustomers] = useState([]);
  const [selectedCustomer, setSelectedCustomer] = useState(null);
  const navigate = useNavigate();

  const { register, handleSubmit, setValue, formState: { errors } } = useForm();

  useEffect(() => {
    const fetchCustomers = async () => {
      try {
        const response = await customerService.getAll();
        setCustomers(response.data);
      } catch (err) {
        setError('Kunde inte hämta kunder');
      }
    };
    fetchCustomers();
  }, []);

  const handleCustomerSelect = (customerId) => {
    const customer = customers.find(c => c.id === parseInt(customerId));
    if (customer) {
      setSelectedCustomer(customer);
      // Populate form with existing customer data
      setValue('firstName', customer.firstName);
      setValue('lastName', customer.lastName);
      setValue('phoneNumber', customer.phoneNumber);
      setValue('email', customer.email || '');
      setValue('address', customer.address || '');
      setValue('city', customer.city || '');
      setValue('postalCode', customer.postalCode || '');
    }
  };

  const onSubmit = async (data) => {
    if (!selectedCustomer) {
      setError('Välj en kund att uppdatera');
      return;
    }

    setLoading(true);
    setError('');
    setSuccess('');

    try {
      const customerData = {
        firstName: data.firstName,
        lastName: data.lastName,
        phoneNumber: data.phoneNumber,
        email: data.email || null,
        address: data.address || null,
        city: data.city || null,
        postalCode: data.postalCode || null
      };

      await customerService.update(selectedCustomer.id, customerData);
      setSuccess('Kund uppdaterad framgångsrikt!');
      
      // Refresh customer list
      const response = await customerService.getAll();
      setCustomers(response.data);
      
      setTimeout(() => {
        navigate('/');
      }, 2000);
    } catch (err) {
      setError(err.response?.data?.message || 'Ett fel uppstod vid uppdatering av kund');
    } finally {
      setLoading(false);
    }
  };

  const validatePhoneNumber = (value) => {
    const phoneRegex = /^[\d\s\-+()]+$/;
    if (!phoneRegex.test(value)) {
      return 'Ogiltigt telefonnummer format';
    }
    return true;
  };

  const validateEmail = (value) => {
    if (!value) return true; // Email is optional
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(value)) {
      return 'Ogiltigt e-postformat';
    }
    return true;
  };

  return (
    <div className="app-container">
      <Header />
      <Container maxWidth="md" sx={{ mt: 4 }}>
      <Paper elevation={3} sx={{ p: 4 }}>
        <Typography variant="h4" component="h1" gutterBottom align="center">
          Uppdatera Kund
        </Typography>

        {error && <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>}
        {success && <Alert severity="success" sx={{ mb: 2 }}>{success}</Alert>}

        <Box sx={{ mt: 2 }}>
          <FormControl fullWidth margin="normal">
            <InputLabel>Välj kund att uppdatera</InputLabel>
            <Select
              label="Välj kund att uppdatera"
              onChange={(e) => handleCustomerSelect(e.target.value)}
              defaultValue=""
            >
              {customers.map((customer) => (
                <MenuItem key={customer.id} value={customer.id}>
                  {customer.firstName} {customer.lastName} - {customer.phoneNumber}
                </MenuItem>
              ))}
            </Select>
          </FormControl>

          {selectedCustomer && (
            <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 3 }}>
              <Typography variant="h6" gutterBottom sx={{ mt: 3 }}>
                Uppdatera information för: {selectedCustomer.firstName} {selectedCustomer.lastName}
              </Typography>

              <TextField
                fullWidth
                label="Förnamn"
                margin="normal"
                {...register('firstName', { 
                  required: 'Förnamn är obligatoriskt',
                  minLength: { value: 2, message: 'Förnamn måste vara minst 2 tecken' }
                })}
                error={!!errors.firstName}
                helperText={errors.firstName?.message}
              />

              <TextField
                fullWidth
                label="Efternamn"
                margin="normal"
                {...register('lastName', { 
                  required: 'Efternamn är obligatoriskt',
                  minLength: { value: 2, message: 'Efternamn måste vara minst 2 tecken' }
                })}
                error={!!errors.lastName}
                helperText={errors.lastName?.message}
              />

              <TextField
                fullWidth
                label="Telefonnummer"
                margin="normal"
                {...register('phoneNumber', { 
                  required: 'Telefonnummer är obligatoriskt',
                  validate: validatePhoneNumber
                })}
                error={!!errors.phoneNumber}
                helperText={errors.phoneNumber?.message}
              />

              <TextField
                fullWidth
                label="E-post"
                type="email"
                margin="normal"
                {...register('email', { validate: validateEmail })}
                error={!!errors.email}
                helperText={errors.email?.message || 'Frivilligt'}
              />

              <TextField
                fullWidth
                label="Adress"
                margin="normal"
                {...register('address')}
                helperText="Frivilligt"
              />

              <TextField
                fullWidth
                label="Stad"
                margin="normal"
                {...register('city')}
                helperText="Frivilligt"
              />

              <TextField
                fullWidth
                label="Postnummer"
                margin="normal"
                {...register('postalCode')}
                helperText="Frivilligt"
              />

              <Box sx={{ mt: 3, display: 'flex', gap: 2 }}>
                <Button
                  type="submit"
                  variant="contained"
                  color="primary"
                  disabled={loading}
                  sx={{ flex: 1 }}
                >
                  {loading ? <CircularProgress size={24} /> : 'Uppdatera Kund'}
                </Button>
                <Button
                  variant="outlined"
                  onClick={() => navigate('/')}
                  sx={{ flex: 1 }}
                >
                  Avbryt
                </Button>
              </Box>
            </Box>
          )}
        </Box>
      </Paper>
    </Container>
    </div>
  );
};

export default UppdateraKund;
