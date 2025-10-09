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
import { journalService, customerService, visitService } from '../services/api';
import { validationRules, sanitizeInput } from '../utils/validation';

const RegistreraJournal = () => {
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [loading, setLoading] = useState(false);
  const [customers, setCustomers] = useState([]);
  const [visits, setVisits] = useState([]);
  const navigate = useNavigate();

  const { register, handleSubmit, watch, formState: { errors } } = useForm();

  const selectedCustomerId = watch('customerId');

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

  useEffect(() => {
    const fetchVisits = async () => {
      if (selectedCustomerId) {
        try {
          const response = await visitService.getAll();
          const customerVisits = response.data.filter(visit => visit.customerId === parseInt(selectedCustomerId));
          setVisits(customerVisits);
        } catch (err) {
          setError('Kunde inte hämta besök');
        }
      } else {
        setVisits([]);
      }
    };
    fetchVisits();
  }, [selectedCustomerId]);

  const onSubmit = async (data) => {
    setLoading(true);
    setError('');
    setSuccess('');

    try {
      // Sanitize input data
      const journalData = {
        customerId: parseInt(data.customerId),
        visitId: parseInt(data.visitId),
        description: sanitizeInput.text(data.description),
        notes: sanitizeInput.text(data.notes),
        createdDate: new Date().toISOString()
      };

      await journalService.create(journalData);
      setSuccess('Journal registrerad framgångsrikt!');
      
      setTimeout(() => {
        navigate('/');
      }, 2000);
    } catch (err) {
      setError(err.response?.data?.message || 'Ett fel uppstod vid registrering av journal');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="app-container">
      <Header />
      <Container maxWidth="md" sx={{ mt: 4 }}>
        
        
      <Paper elevation={3} sx={{ p: 4 }}>
        <Typography variant="h4" component="h1" gutterBottom align="center">
          Registrera Journal
        </Typography>

        {error && <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>}
        {success && <Alert severity="success" sx={{ mb: 2 }}>{success}</Alert>}

        <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 2 }}>
          <FormControl fullWidth margin="normal" error={!!errors.customerId}>
            <InputLabel>Kund</InputLabel>
            <Select
              {...register('customerId', { required: 'Kund måste väljas' })}
              label="Kund"
              defaultValue=""
            >
              {customers.map((customer) => (
                <MenuItem key={customer.id} value={customer.id}>
                  {customer.firstName} {customer.lastName} - {customer.phoneNumber}
                </MenuItem>
              ))}
            </Select>
            {errors.customerId && (
              <Typography variant="caption" color="error">
                {errors.customerId.message}
              </Typography>
            )}
          </FormControl>

          <FormControl fullWidth margin="normal" error={!!errors.visitId}>
            <InputLabel>Besök</InputLabel>
            <Select
              {...register('visitId', { required: 'Besök måste väljas' })}
              label="Besök"
              defaultValue=""
              disabled={!selectedCustomerId || visits.length === 0}
            >
              {visits.map((visit) => (
                <MenuItem key={visit.id} value={visit.id}>
                  {new Date(visit.visitDate).toLocaleDateString('sv-SE')} - {visit.description}
                </MenuItem>
              ))}
            </Select>
            {errors.visitId && (
              <Typography variant="caption" color="error">
                {errors.visitId.message}
              </Typography>
            )}
            {selectedCustomerId && visits.length === 0 && (
              <Typography variant="caption" color="info">
                Inga besök finns för vald kund
              </Typography>
            )}
          </FormControl>

          <TextField
            fullWidth
            label="Beskrivning"
            margin="normal"
            multiline
            rows={4}
            {...register('description', { 
              ...validationRules.description,
              required: 'Beskrivning är obligatorisk',
              minLength: { value: 10, message: 'Beskrivning måste vara minst 10 tecken' }
            })}
            error={!!errors.description}
            helperText={errors.description?.message}
          />

          <TextField
            fullWidth
            label="Anteckningar"
            margin="normal"
            multiline
            rows={3}
            {...register('notes', validationRules.description)}
            error={!!errors.notes}
            helperText={errors.notes?.message || "Frivilliga anteckningar"}
          />

          <Box sx={{ mt: 3, display: 'flex', gap: 2 }}>
            <Button
              type="submit"
              variant="contained"
              color="primary"
              disabled={loading}
              sx={{ flex: 1 }}
            >
              {loading ? <CircularProgress size={24} /> : 'Registrera Journal'}
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
      </Paper>
    </Container>
    </div>
  );
};

export default RegistreraJournal;
