import React, { useState, useEffect } from 'react';
import { useRecoilValue } from 'recoil';
import { orderStatusesState, userInfoState } from '../../shared/state/atom';
import { motion, AnimatePresence } from 'framer-motion';
import { FaInfoCircle, FaArrowLeft, FaMapMarkerAlt, FaBox, FaTruck, FaMoneyBill } from 'react-icons/fa';
import tutorialImage from '../../assets/koi-koi-about-us.webp';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const initialOrderState = {
  customerId: 0,
  pickupLocation: '',
  destination: '',
  weight: '',
  quantity: '1',
  total: 0,
  transportMethod: '',
  pricingId: '',
  additionalServices: '',
  pickupCity: '',
  pickupDistrict: '',
  pickupDetailedAddress: '',
  destinationCity: '',
  destinationDistrict: '', 
  destinationDetailedAddress: ''
};

const CreateOrder: React.FC = () => {
  const orderStatuses = useRecoilValue(orderStatusesState);
  const userInfo = useRecoilValue(userInfoState);
  const navigate = useNavigate();

  const [isLoading, setIsLoading] = useState(false);
  const [currentStep, setCurrentStep] = useState(1);
  const [order, setOrder] = useState({
    ...initialOrderState,
    customerId: userInfo?.id || 0
  });

  const [pricingOptions, setPricingOptions] = useState<
    { priceId: number; transportMethod: string; pricePerKg: number; weightRange: string }[]
  >([]);

  const [vietnamLocations, setVietnamLocations] = useState({});

  useEffect(() => {
    const fetchPricingOptions = async () => {
      try {
        const response = await axios.get(
          `${import.meta.env.VITE_API_URL}/Price/get-prices`
        );
        setPricingOptions(response.data.data);
      } catch (error) {
        console.error('Error fetching pricing options:', error);
        toast.error('Failed to load pricing options. Please refresh the page.');
      }
    };

    fetchPricingOptions();
  }, []);

  useEffect(() => {
    const fetchLocations = async () => {
      try {
        const response = await axios.get('https://provinces.open-api.vn/api/?depth=2');
        const locations = {};
        
        response.data.forEach((province) => {
          const provinceName = province.name;
          locations[provinceName] = {};
          
          province.districts.forEach((district) => {
            const districtName = district.name;
            locations[provinceName][districtName] = [];
          });
        });

        setVietnamLocations(locations);
      } catch (error) {
        console.error('Error fetching locations:', error);
      }
    };

    fetchLocations();
  }, []);

  const validateStep = (step: number) => {
    switch (step) {
      case 1:
        if (!order.weight || parseFloat(order.weight) <= 0) {
          toast.warning('Please enter a valid weight');
          return false;
        }
        if (!order.quantity || parseInt(order.quantity) <= 0) {
          toast.warning('Please enter a valid quantity');
          return false;  
        }
        if (!order.pricingId) {
          toast.warning('Please select a shipping method');
          return false;
        }
        return true;
      case 2:
        if (!order.pickupCity || !order.pickupDistrict || !order.pickupDetailedAddress) {
          toast.warning('Please complete all pickup location fields');
          return false;
        }
        return true;
      case 3:
        if (!order.destinationCity || !order.destinationDistrict || !order.destinationDetailedAddress) {
          toast.warning('Please complete all destination location fields');
          return false;
        }
        return true;
      case 4:
        return true;
      default:
        return true;
    }
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    
    setOrder((prevOrder) => {
      const updatedOrder = {
        ...prevOrder,
        [name]: value,
      };

      // Reset dependent fields when city/district changes
      if (name === 'pickupCity') {
        updatedOrder.pickupDistrict = '';
        updatedOrder.pickupDetailedAddress = '';
      }
      if (name === 'pickupDistrict') {
        // No longer resetting pickupWard
      }
      if (name === 'destinationCity') {
        updatedOrder.destinationDistrict = '';
        updatedOrder.destinationDetailedAddress = '';
      }
      if (name === 'destinationDistrict') {
        // No longer resetting destinationWard
      }

      // Update combined addresses only if all required fields are present
      if (name.startsWith('pickup') && 
          updatedOrder.pickupDetailedAddress && 
          updatedOrder.pickupDistrict && 
          updatedOrder.pickupCity) {
        updatedOrder.pickupLocation = `${updatedOrder.pickupDetailedAddress}, ${updatedOrder.pickupDistrict}, ${updatedOrder.pickupCity}`.trim();
      }

      if (name.startsWith('destination') &&
          updatedOrder.destinationDetailedAddress &&
          updatedOrder.destinationDistrict &&
          updatedOrder.destinationCity) {
        updatedOrder.destination = `${updatedOrder.destinationDetailedAddress}, ${updatedOrder.destinationDistrict}, ${updatedOrder.destinationCity}`.trim();
      }

      // Calculate total when weight or pricing changes
      if (['weight', 'pricingId'].includes(name)) {
        const selectedPricing = pricingOptions.find(
          (option) => option.priceId === parseInt(updatedOrder.pricingId)
        );
        const weight = parseFloat(updatedOrder.weight) || 0;
        const quantity = parseInt(updatedOrder.quantity) || 1;
        
        if (selectedPricing && weight > 0) {
          updatedOrder.total = selectedPricing.pricePerKg * weight * quantity;
          updatedOrder.transportMethod = selectedPricing.transportMethod;
        } else {
          updatedOrder.total = 0;
          updatedOrder.transportMethod = '';
        }
      }

      return updatedOrder;
    });
  };

  const nextStep = () => {
    if (validateStep(currentStep)) {
      setCurrentStep(prev => Math.min(prev + 1, 4));
    }
  };

  const prevStep = () => {
    setCurrentStep(prev => Math.max(prev - 1, 1));
  };

  const resetForm = () => {
    setOrder({
      ...initialOrderState,
      customerId: userInfo?.id || 0
    });
    setCurrentStep(1);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    // Validate all steps before submission
    for (let step = 1; step <= 4; step++) {
      if (!validateStep(step)) {
        setCurrentStep(step);
        return;
      }
    }

    setIsLoading(true);
    try {
      const orderData = {
        customerId: order.customerId,
        pickupLocation: order.pickupLocation,
        destination: order.destination,
        weight: parseFloat(order.weight),
        quantity: parseInt(order.quantity),
        total: order.total,
        transportMethod: order.transportMethod,
        pricingId: parseInt(order.pricingId),
        additionalServices: order.additionalServices.trim()
      };

      const response = await axios.post(
        `${import.meta.env.VITE_API_URL}/KoiOrder/create-order`,
        orderData
      );

      if (response.data.success) {
        toast.success('Order created successfully!');
        const newOrder = response.data.data;
        
        if (newOrder.status === 'WaitingForPayment') {
          try {
            const paymentResponse = await axios.post(
              `${import.meta.env.VITE_API_URL}/VNPay/Payment`,
              null,
              { params: { orderId: newOrder.orderId } }
            );
            
            if (paymentResponse.data.paymentUrl) {
              window.open(paymentResponse.data.paymentUrl, '_blank');
            } else {
              toast.error('Payment URL not received');
            }
          } catch (paymentError) {
            console.error('Payment error:', paymentError);
            toast.error('Failed to initiate payment');
          }
        }
        
        resetForm();
      } else {
        toast.error(response.data.message || 'Failed to create order');
      }
    } catch (error) {
      console.error('Error creating order:', error);
      toast.error('Failed to create order. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  const renderStepContent = () => {
    switch (currentStep) {
      case 1:
        return (
          <div className="space-y-4">
            <motion.h3 
              className="text-lg font-semibold text-blue-600 flex items-center"
              initial={{ x: -20, opacity: 0 }}
              animate={{ x: 0, opacity: 1 }}
              transition={{ duration: 0.5 }}
            >
              <FaBox className="mr-2" />
              Package Details & Shipping Method
            </motion.h3>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label htmlFor="weight" className="block text-sm font-medium mb-1">
                  Weight (kg) <span className="text-red-500">*</span>
                </label>
                <input
                  type="number"
                  id="weight"
                  name="weight"
                  value={order.weight}
                  onChange={handleChange}
                  min="0.1"
                  step="0.1"
                  className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                  required
                />
              </div>

              <div>
                <label htmlFor="quantity" className="block text-sm font-medium mb-1">
                  Quantity <span className="text-red-500">*</span>
                </label>
                <input
                  type="number"
                  id="quantity"
                  name="quantity"
                  value={order.quantity}
                  onChange={handleChange}
                  min="1"
                  className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                  required
                />
              </div>

              <div className="md:col-span-2">
                <label htmlFor="pricingId" className="block text-sm font-medium mb-1">
                  Select Shipping Method <span className="text-red-500">*</span>
                </label>
                <select
                  id="pricingId"
                  name="pricingId"
                  value={order.pricingId}
                  onChange={handleChange}
                  className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                  required
                >
                  <option value="">Choose shipping method</option>
                  {pricingOptions.map((option) => (
                    <option key={option.priceId} value={option.priceId}>
                      {option.transportMethod} - {option.pricePerKg.toLocaleString('vi-VN')}đ/kg - {option.weightRange}
                    </option>
                  ))}
                </select>
              </div>

              <div className="md:col-span-2">
                <label htmlFor="additionalServices" className="block text-sm font-medium mb-1">
                  Additional Services
                </label>
                <input
                  type="text"
                  id="additionalServices"
                  name="additionalServices"
                  value={order.additionalServices}
                  onChange={handleChange}
                  placeholder="Enter any additional services needed"
                  className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                />
              </div>

              {order.total > 0 && (
                <div className="md:col-span-2 bg-blue-50 p-4 rounded-lg">
                  <p className="font-semibold text-blue-600">
                    Estimated Total: {order.total.toLocaleString('vi-VN')}đ
                  </p>
                </div>
              )}
            </div>
          </div>
        );
      case 2:
        return (
          <div className="space-y-4">
            <motion.h3 
              className="text-lg font-semibold text-blue-600 flex items-center"
              initial={{ x: -20, opacity: 0 }}
              animate={{ x: 0, opacity: 1 }}
              transition={{ duration: 0.5 }}
            >
              <FaMapMarkerAlt className="mr-2" />
              Pickup Location
            </motion.h3>
            <div className="grid grid-cols-1 gap-4">
              <div>
                <label htmlFor="pickupCity" className="block text-sm font-medium mb-1">
                  City <span className="text-red-500">*</span>
                </label>
                <select
                  id="pickupCity"
                  name="pickupCity"
                  value={order.pickupCity}
                  onChange={handleChange}
                  className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                  required
                >
                  <option value="">Select City</option>
                  {Object.keys(vietnamLocations).map((city) => (
                    <option key={city} value={city}>{city}</option>
                  ))}
                </select>
              </div>

              <div>
                <label htmlFor="pickupDistrict" className="block text-sm font-medium mb-1">
                  District <span className="text-red-500">*</span>
                </label>
                <select
                  id="pickupDistrict"
                  name="pickupDistrict"
                  value={order.pickupDistrict}
                  onChange={handleChange}
                  className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                  required
                  disabled={!order.pickupCity}
                >
                  <option value="">Select District</option>
                  {order.pickupCity && vietnamLocations[order.pickupCity] &&
                    Object.keys(vietnamLocations[order.pickupCity]).map((district) => (
                      <option key={district} value={district}>{district}</option>
                    ))
                  }
                </select>
              </div>

              <div>
                <label htmlFor="pickupDetailedAddress" className="block text-sm font-medium mb-1">
                  Detailed Address <span className="text-red-500">*</span>
                </label>
                <input
                  type="text"
                  id="pickupDetailedAddress"
                  name="pickupDetailedAddress"
                  value={order.pickupDetailedAddress}
                  onChange={handleChange}
                  className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                  required
                  placeholder="House number, street name, etc."
                  title="Enter detailed address including house number and street name"
                />
              </div>
            </div>
          </div>
        );
      case 3:
        return (
          <div className="space-y-4">
            <motion.h3 
              className="text-lg font-semibold text-blue-600 flex items-center"
              initial={{ x: -20, opacity: 0 }}
              animate={{ x: 0, opacity: 1 }}
              transition={{ duration: 0.5 }}
            >
              <FaMapMarkerAlt className="mr-2" />
              Destination Location
            </motion.h3>
            <div className="grid grid-cols-1 gap-4">
              <div>
                <label htmlFor="destinationCity" className="block text-sm font-medium mb-1">
                  City <span className="text-red-500">*</span>
                </label>
                <select
                  id="destinationCity"
                  name="destinationCity"
                  value={order.destinationCity}
                  onChange={handleChange}
                  className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                  required
                >
                  <option value="">Select City</option>
                  {Object.keys(vietnamLocations).map((city) => (
                    <option key={city} value={city}>{city}</option>
                  ))}
                </select>
              </div>

              <div>
                <label htmlFor="destinationDistrict" className="block text-sm font-medium mb-1">
                  District <span className="text-red-500">*</span>
                </label>
                <select
                  id="destinationDistrict"
                  name="destinationDistrict"
                  value={order.destinationDistrict}
                  onChange={handleChange}
                  className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                  required
                  disabled={!order.destinationCity}
                >
                  <option value="">Select District</option>
                  {order.destinationCity && vietnamLocations[order.destinationCity] &&
                    Object.keys(vietnamLocations[order.destinationCity]).map((district) => (
                      <option key={district} value={district}>{district}</option>
                    ))
                  }
                </select>
              </div>

              <div>
                <label htmlFor="destinationDetailedAddress" className="block text-sm font-medium mb-1">
                  Detailed Address <span className="text-red-500">*</span>
                </label>
                <input
                  type="text"
                  id="destinationDetailedAddress"
                  name="destinationDetailedAddress"
                  value={order.destinationDetailedAddress}
                  onChange={handleChange}
                  className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                  required
                  placeholder="House number, street name, etc."
                  title="Enter detailed address including house number and street name"
                />
              </div>
            </div>
          </div>
        );
      case 4:
        return (
          <div className="space-y-4">
            <motion.h3 
              className="text-lg font-semibold text-blue-600 flex items-center"
              initial={{ x: -20, opacity: 0 }}
              animate={{ x: 0, opacity: 1 }}
              transition={{ duration: 0.5 }}
            >
              <FaTruck className="mr-2" />
              Review and Confirm Order
            </motion.h3>
            
            <div className="bg-blue-50 p-6 rounded-lg space-y-4">
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <h4 className="font-semibold mb-2">Package Details</h4>
                  <p>Weight: {order.weight}kg</p>
                  <p>Quantity: {order.quantity}</p>
                  <p>Shipping Method: {order.transportMethod}</p>
                  {order.additionalServices && (
                    <p>Additional Services: {order.additionalServices}</p>
                  )}
                </div>
                
                <div>
                  <h4 className="font-semibold mb-2">Pickup Location</h4>
                  <p>{order.pickupDetailedAddress}</p>
                  <p>{order.pickupDistrict}</p>
                  <p>{order.pickupCity}</p>
                </div>
                
                <div>
                  <h4 className="font-semibold mb-2">Destination</h4>
                  <p>{order.destinationDetailedAddress}</p>
                  <p>{order.destinationDistrict}</p>
                  <p>{order.destinationCity}</p>
                </div>
                
                <div>
                  <h4 className="font-semibold mb-2">Payment</h4>
                  <p className="text-2xl font-bold text-blue-600">
                    {order.total.toLocaleString('vi-VN')}đ
                  </p>
                </div>
              </div>

              <div className="mt-6 p-4 bg-yellow-50 rounded-lg border border-yellow-200">
                <p className="text-yellow-700 flex items-center">
                  <FaInfoCircle className="mr-2" />
                  Please review all details carefully before confirming your order
                </p>
              </div>
            </div>
          </div>
        );
      default:
        return null;
    }
  };

  return (
    <div className="container mx-auto px-4 py-8 min-h-[calc(100vh-96px)]">
      <div className="flex justify-between items-center mb-6">
        <motion.h1
          className="text-3xl font-semibold"
          initial={{ opacity: 0, y: -20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5 }}
        >
          Create New Order
        </motion.h1>
        <motion.button
          className="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600 transition-colors flex items-center"
          onClick={() => navigate(-1)}
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ duration: 0.5 }}
          disabled={isLoading}
        >
          <FaArrowLeft className="mr-2" />
          Back
        </motion.button>
      </div>

      {/* Progress Steps */}
      <div className="mb-8">
        <div className="flex justify-between items-center">
          {[1, 2, 3, 4].map((step) => (
            <motion.div 
              key={step} 
              className="flex flex-col items-center"
              animate={{
                scale: currentStep === step ? 1.1 : 1,
                opacity: currentStep >= step ? 1 : 0.5
              }}
              transition={{ duration: 0.3 }}
            >
              <div className={`w-8 h-8 rounded-full flex items-center justify-center ${
                currentStep >= step ? 'bg-blue-500 text-white' : 'bg-gray-200'
              }`}>
                {step}
              </div>
              <span className="text-sm mt-1 font-medium">
                {step === 1 ? 'Package & Method' :
                 step === 2 ? 'Pickup' :
                 step === 3 ? 'Destination' :
                 'Review & Confirm'}
              </span>
            </motion.div>
          ))}
        </div>
        <div className="relative mt-2">
          <div className="absolute top-0 left-0 h-1 bg-gray-200 w-full">
            <motion.div
              className="h-full bg-blue-500"
              initial={{ width: 0 }}
              animate={{ width: `${((currentStep - 1) / 3) * 100}%` }}
              transition={{ duration: 0.5 }}
            />
          </div>
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
        <motion.div
          className="flex items-center justify-center"
          initial={{ opacity: 0, scale: 0.9 }}
          animate={{ opacity: 1, scale: 1 }}
          transition={{ duration: 0.5 }}
        >
          <img
            src={tutorialImage}
            alt="Order Creation Guide"
            className="rounded-lg shadow-lg max-w-full h-auto"
          />
        </motion.div>

        <motion.div
          className="bg-white p-6 rounded-lg shadow-lg border-2 border-blue-500"
          initial={{ opacity: 0, scale: 0.9 }}
          animate={{ opacity: 1, scale: 1 }}
          transition={{ duration: 0.5 }}
        >
          <form onSubmit={handleSubmit} className="space-y-6">
            <AnimatePresence mode="wait">
              <motion.div
                key={currentStep}
                initial={{ opacity: 0, x: 20 }}
                animate={{ opacity: 1, x: 0 }}
                exit={{ opacity: 0, x: -20 }}
                transition={{ duration: 0.3 }}
              >
                {renderStepContent()}
              </motion.div>
            </AnimatePresence>

            <div className="flex justify-between mt-6">
              {currentStep > 1 && (
                <motion.button
                  type="button"
                  onClick={prevStep}
                  className="px-6 py-2 bg-gray-500 text-white rounded-md hover:bg-gray-600"
                  whileHover={{ scale: 1.02 }}
                  whileTap={{ scale: 0.98 }}
                >
                  Previous
                </motion.button>
              )}
              
              {currentStep < 4 ? (
                <motion.button
                  type="button"
                  onClick={nextStep}
                  className="px-6 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 ml-auto"
                  whileHover={{ scale: 1.02 }}
                  whileTap={{ scale: 0.98 }}
                >
                  Next
                </motion.button>
              ) : (
                <motion.button
                  type="submit"
                  className={`px-6 py-2 rounded-md text-white font-semibold ml-auto
                    ${isLoading 
                      ? 'bg-gray-400 cursor-not-allowed'
                      : 'bg-gradient-to-r from-blue-500 to-blue-600 hover:from-blue-600 hover:to-blue-700'
                    }`}
                  disabled={isLoading}
                  whileHover={{ scale: isLoading ? 1 : 1.02 }}
                  whileTap={{ scale: isLoading ? 1 : 0.98 }}
                >
                  {isLoading ? 'Creating Order...' : 'Confirm & Create Order'}
                </motion.button>
              )}
            </div>
          </form>
        </motion.div>
      </div>
    </div>
  );
};

export default CreateOrder;
