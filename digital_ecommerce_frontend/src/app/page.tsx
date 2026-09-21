"use client";
import Image from "next/image";
import { useDispatch, useSelector } from "react-redux";
import { increment, decrement, incrementBy, reset } from "../redux/slices/counterSlice";
import Navbar from "./components/Navbar";
import { useGetProductsWithPaginationMutation } from "../apis/productApi";
import { useState, useEffect } from "react";
import ProductCard from "./components/ProductCard";

export default function Home() {
  const [paginationModel, setPaginationModel] = useState({
    page: 1,
    pageSize: 10,
    keyword: '',
  });

  const [product, setProduct] = useState([]);

  // 1. Hook'tan tetikleyici fonksiyonu ve istek durumlarını çekiyoruz:
  const [getProductsWithPagination, { data, isLoading, isError }] = useGetProductsWithPaginationMutation();

  // 2. Sayfa açıldığında veya paginationModel değiştiğinde isteği atıyoruz:
  useEffect(() => {
    const fetchProducts = async () => {
      try{
        const response = await getProductsWithPagination(paginationModel).unwrap();;
        console.log(response);
        setProduct(response.result?.items);
      }catch(error){
        console.error('Error occured when when fetching products', error);
      }
    };
    fetchProducts();
  },[])
      

  if (isLoading) return <div className="p-8 text-center text-lg">Ürünler Yükleniyor...</div>;
  if (isError) return <div className="p-8 text-center text-red-500">Ürünler yüklenirken bir hata oluştu!</div>;

  return (
    <main className="container mx-auto p-4 min-h-screen">
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
        {product?.map((product: any) => (
          <ProductCard key={product.id} product={product} />
        ))}
      </div>
    </main>
  );
}
