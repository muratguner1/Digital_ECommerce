'use client';

import React, { useState } from 'react';
import { useParams } from 'next/navigation';
import Link from 'next/link';
import { 
  FiShoppingCart, 
  FiHeart, 
  FiStar, 
  FiChevronLeft, 
  FiChevronRight, 
  FiCheck, 
  FiTruck, 
  FiShield, 
  FiRefreshCw, 
  FiCheckCircle, 
  FiXCircle 
} from 'react-icons/fi';
import toast from 'react-hot-toast';
import { useGetProductDetailByIdQuery } from '../../../apis/productApi';
import { Domain_URL } from '../../../constants/url';
import styles from './productDetail.module.css';

// Renk isimlerini hex kodlarına eşleyen yardımcı harita
const COLOR_HEX_MAP = {
  'siyah': '#1f2937',
  'black': '#1f2937',
  'beyaz': '#f9fafb',
  'white': '#ffffff',
  'mavi': '#2563eb',
  'blue': '#2563eb',
  'kırmızı': '#dc2626',
  'red': '#dc2626',
  'yeşil': '#16a34a',
  'green': '#16a34a',
  'gri': '#6b7280',
  'gray': '#6b7280',
  'grey': '#6b7280',
  'uzay grisi': '#374151',
  'space gray': '#374151',
  'gümüş': '#e5e7eb',
  'silver': '#e5e7eb',
  'altın': '#f59e0b',
  'gold': '#f59e0b',
  'mor': '#9333ea',
  'purple': '#9333ea',
  'sarı': '#eab308',
  'yellow': '#eab308',
};

function ProductDetailPage() {
  const params = useParams();
  const id = params?.id;

  // RTK Query ile ürünü çekiyoruz
  const { data, isLoading, isError } = useGetProductDetailByIdQuery(id, {
    skip: !id,
  });

  const product = data?.result || data;

  // State'ler
  const [selectedImageIndex, setSelectedImageIndex] = useState(0);
  const [selectedColorState, setSelectedColor] = useState('');
  const [quantity, setQuantity] = useState(1);
  const [isFavorite, setIsFavorite] = useState(false);
  const [activeTab, setActiveTab] = useState('description');

  // Sıralı Fotoğraflar Listesi
  const sortedImages = React.useMemo(() => {
    if (!product || !product.productImages || product.productImages.length === 0) return [];
    return [...product.productImages].sort((a, b) => {
      const urlA = a.imageUrl || '';
      const urlB = b.imageUrl || '';
      return urlA.localeCompare(urlB, undefined, { numeric: true, sensitivity: 'base' });
    });
  }, [product]);

  // Renk seçenekleri
  const availableColors = React.useMemo(() => {
    if (!product || !product.color) {
      return ['Uzay Grisi', 'Gümüş', 'Gece Mavisi'];
    }
    if (product.color.includes(',')) {
      return product.color.split(',').map((c) => c.trim());
    }
    return [product.color];
  }, [product]);

  const selectedColor = selectedColorState || availableColors[0] || '';

  // Resim URL temizleme fonksiyonu
  const getImageUrl = (rawPath) => {
    if (!rawPath) return 'https://clipground.com/images/cell-phones-clipart-12.jpg';
    if (rawPath.startsWith('http://') || rawPath.startsWith('https://')) {
      return rawPath;
    }
    const cleanPath = rawPath
      .replace(/^wwwroot[\\/]/, '')
      .replace(/\\/g, '/')
      .replace(/^\/+/, '');
    return `${Domain_URL}${cleanPath}`;
  };

  // Fiyat formatlama
  const formatPrice = (price) => {
    return new Intl.NumberFormat('tr-TR', {
      style: 'currency',
      currency: 'TRY',
      minimumFractionDigits: 0,
      maximumFractionDigits: 0,
    }).format(price || 0);
  };

  // Sepete Ekleme
  const handleAddToCart = () => {
    toast.success(`${quantity} adet ${product?.productName} (${selectedColor}) sepete eklendi!`, {
      icon: '🛒',
      duration: 3000,
    });
  };

  // Favoriye Ekleme
  const handleToggleFavorite = () => {
    setIsFavorite((prev) => !prev);
    if (!isFavorite) {
      toast.success('Favorilere eklendi!', { icon: '❤️' });
    } else {
      toast('Favorilerden çıkarıldı.', { icon: '💔' });
    }
  };

  // Fotoğraf Galerisi Gezinme
  const handlePrevImage = () => {
    setSelectedImageIndex((prev) => (prev === 0 ? sortedImages.length - 1 : prev - 1));
  };

  const handleNextImage = () => {
    setSelectedImageIndex((prev) => (prev === sortedImages.length - 1 ? 0 : prev + 1));
  };

  // Yüklenme Ekranı (Loading)
  if (isLoading) {
    return (
      <div className={styles.loadingContainer}>
        <div className={styles.spinner}></div>
      </div>
    );
  }

  // Hata veya Ürün Bulunamadı Ekranı
  if (isError || !product) {
    return (
      <div className={styles.errorContainer}>
        <h2 className={styles.errorTitle}>Ürün Bulunamadı</h2>
        <p className={styles.errorText}>
          Aradığınız ürün mevcut değil veya kaldırılmış olabilir.
        </p>
        <Link href="/" className={styles.backBtn}>
          &larr; Anasayfaya Dön
        </Link>
      </div>
    );
  }

  // Fiyat & İndirim Hesabı
  const hasDiscount = Boolean(product.discount && product.discount > 0);
  const discountedPrice = hasDiscount
    ? product.productPrice - (product.productPrice * product.discount) / 100
    : product.productPrice;

  const currentDisplayImage = sortedImages[selectedImageIndex]?.imageUrl
    ? getImageUrl(sortedImages[selectedImageIndex].imageUrl)
    : 'https://clipground.com/images/cell-phones-clipart-12.jpg';

  const inStock = (product.productStock ?? 1) > 0;

  return (
    <div className={styles.container}>
      {/* Breadcrumb / Gezinti Yolu */}
      <nav className={styles.breadcrumb} aria-label="Breadcrumb">
        <Link href="/" className={styles.breadcrumbLink}>Anasayfa</Link>
        <span>/</span>
        <Link href="/" className={styles.breadcrumbLink}>Telefonlar</Link>
        <span>/</span>
        <span className={styles.breadcrumbActive}>{product.productName}</span>
      </nav>

      {/* Üst Kısım: Galeri + Detaylar Grid */}
      <div className={styles.productGrid}>
        
        {/* SOL TARAF: Görsel Galerisi */}
        <div className={styles.gallerySection}>
          <div className={styles.mainImageWrapper}>
            {hasDiscount && (
              <span className={styles.badgeDiscount}>-%{product.discount} İndirim</span>
            )}

            <img
              src={currentDisplayImage}
              alt={product.productName}
              className={styles.mainImage}
              onError={(e) => {
                e.currentTarget.src = 'https://clipground.com/images/cell-phones-clipart-12.jpg';
              }}
            />

            {/* Galeri Okları (1'den fazla resim varsa) */}
            {sortedImages.length > 1 && (
              <>
                <button
                  type="button"
                  className={`${styles.navArrow} ${styles.navArrowLeft}`}
                  onClick={handlePrevImage}
                  aria-label="Önceki Görsel"
                >
                  <FiChevronLeft size={22} />
                </button>
                <button
                  type="button"
                  className={`${styles.navArrow} ${styles.navArrowRight}`}
                  onClick={handleNextImage}
                  aria-label="Sonraki Görsel"
                >
                  <FiChevronRight size={22} />
                </button>
              </>
            )}
          </div>

          {/* Küçük Resimler (Thumbnails) */}
          {sortedImages.length > 1 && (
            <div className={styles.thumbnails}>
              {sortedImages.map((img, idx) => {
                const thumbUrl = getImageUrl(img.imageUrl);
                return (
                  <button
                    key={img.id || idx}
                    type="button"
                    className={`${styles.thumbnailButton} ${
                      selectedImageIndex === idx ? styles.thumbnailActive : ''
                    }`}
                    onClick={() => setSelectedImageIndex(idx)}
                  >
                    <img
                      src={thumbUrl}
                      alt={`Küçük Görsel ${idx + 1}`}
                      className={styles.thumbnailImage}
                      onError={(e) => {
                        e.currentTarget.src = 'https://clipground.com/images/cell-phones-clipart-12.jpg';
                      }}
                    />
                  </button>
                );
              })}
            </div>
          )}
        </div>

        {/* SAĞ TARAF: Ürün Detayları & Satın Alma */}
        <div className={styles.detailsSection}>
          <h1 className={styles.productTitle}>{product.productName}</h1>

          {/* Değerlendirme & Stok Durumu */}
          <div className={styles.ratingStockRow}>
            <div className={styles.rating}>
              {[...Array(5)].map((_, i) => (
                <FiStar
                  key={i}
                  size={18}
                  fill={i < Math.floor(product.rating || 4.5) ? '#FFC107' : 'none'}
                  stroke={i < Math.floor(product.rating || 4.5) ? '#FFC107' : '#9ca3af'}
                />
              ))}
              <span className={styles.ratingText}>
                {product.rating ? product.rating.toFixed(1) : '4.8'} (24 Değerlendirme)
              </span>
            </div>

            <div
              className={`${styles.stockBadge} ${
                inStock ? styles.inStock : styles.outOfStock
              }`}
            >
              {inStock ? (
                <>
                  <FiCheckCircle size={15} />
                  <span>Stokta Var ({product.productStock ?? 'Sınırlı Sayıda'})</span>
                </>
              ) : (
                <>
                  <FiXCircle size={15} />
                  <span>Tükendi</span>
                </>
              )}
            </div>
          </div>

          {/* Fiyat Kutusu */}
          <div className={styles.priceBox}>
            <span className={styles.currentPrice}>
              {formatPrice(discountedPrice)}
            </span>
            {hasDiscount && (
              <>
                <span className={styles.oldPrice}>
                  {formatPrice(product.productPrice)}
                </span>
                <span className={styles.saveBadge}>
                  Kazancınız: {formatPrice(product.productPrice - discountedPrice)}
                </span>
              </>
            )}
          </div>

          {/* Renk Seçenekleri */}
          <div className={styles.colorSection}>
            <label className={styles.sectionLabel}>
              Renk: <span className={styles.colorHighlight}>{selectedColor}</span>
            </label>
            <div className={styles.colorList}>
              {availableColors.map((colorName) => {
                const lower = colorName.toLowerCase();
                const hex = COLOR_HEX_MAP[lower] || '#6b7280';
                const isSelected = selectedColor === colorName;

                return (
                  <button
                    key={colorName}
                    type="button"
                    onClick={() => setSelectedColor(colorName)}
                    className={`${styles.colorOption} ${
                      isSelected ? styles.colorOptionActive : ''
                    }`}
                  >
                    <span
                      className={styles.colorDot}
                      style={{ backgroundColor: hex }}
                    />
                    <span>{colorName}</span>
                    {isSelected && <FiCheck size={16} />}
                  </button>
                );
              })}
            </div>
          </div>

          {/* Adet & Eylemler */}
          <div className={styles.actionRow}>
            <div className={styles.quantitySelector}>
              <button
                type="button"
                className={styles.quantityBtn}
                onClick={() => setQuantity((q) => Math.max(1, q - 1))}
                disabled={quantity <= 1}
              >
                -
              </button>
              <span className={styles.quantityValue}>{quantity}</span>
              <button
                type="button"
                className={styles.quantityBtn}
                onClick={() => setQuantity((q) => q + 1)}
                disabled={product.productStock && quantity >= product.productStock}
              >
                +
              </button>
            </div>

            <button
              type="button"
              className={styles.addToCartBtn}
              onClick={handleAddToCart}
              disabled={!inStock}
            >
              <FiShoppingCart size={20} />
              <span>Sepete Ekle</span>
            </button>

            <button
              type="button"
              className={`${styles.favoriteBtn} ${
                isFavorite ? styles.favoriteActive : ''
              }`}
              onClick={handleToggleFavorite}
              aria-label="Favorilere Ekle"
            >
              <FiHeart
                size={22}
                fill={isFavorite ? '#ef4444' : 'none'}
                stroke={isFavorite ? '#ef4444' : 'currentColor'}
              />
            </button>
          </div>

          {/* Avantajlar & Güvenceler */}
          <div className={styles.perksGrid}>
            <div className={styles.perkCard}>
              <FiTruck className={styles.perkIcon} />
              <div>
                <strong>Ücretsiz Kargo</strong>
                <p style={{ margin: 0, fontSize: '0.75rem', color: '#6b7280' }}>24 Saatte Kargoda</p>
              </div>
            </div>
            <div className={styles.perkCard}>
              <FiRefreshCw className={styles.perkIcon} />
              <div>
                <strong>Kolay İade</strong>
                <p style={{ margin: 0, fontSize: '0.75rem', color: '#6b7280' }}>14 Gün İçinde</p>
              </div>
            </div>
            <div className={styles.perkCard}>
              <FiShield className={styles.perkIcon} />
              <div>
                <strong>Garantili Ürün</strong>
                <p style={{ margin: 0, fontSize: '0.75rem', color: '#6b7280' }}>2 Yıl Distribütör</p>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* ALT BÖLÜM: Sekmeler (Açıklama, Teknik Özellikler, Teslimat) */}
      <div className={styles.bottomSection}>
        <div className={styles.tabList}>
          <button
            type="button"
            className={`${styles.tabBtn} ${
              activeTab === 'description' ? styles.tabBtnActive : ''
            }`}
            onClick={() => setActiveTab('description')}
          >
            Ürün Açıklaması
          </button>
          <button
            type="button"
            className={`${styles.tabBtn} ${
              activeTab === 'specs' ? styles.tabBtnActive : ''
            }`}
            onClick={() => setActiveTab('specs')}
          >
            Teknik Özellikler
          </button>
          <button
            type="button"
            className={`${styles.tabBtn} ${
              activeTab === 'shipping' ? styles.tabBtnActive : ''
            }`}
            onClick={() => setActiveTab('shipping')}
          >
            Teslimat ve İade
          </button>
        </div>

        <div className={styles.tabContent}>
          {activeTab === 'description' && (
            <p style={{ whiteSpace: 'pre-line' }}>
              {product.productDescription ||
                'Bu ürün üstün performans, şık tasarım ve yüksek kaliteli malzeme standartları ile üretilmiştir. Günlük kullanımda ve profesyonel işlerinizde en yüksek verimi sağlamak için tasarlanmıştır.'}
            </p>
          )}

          {activeTab === 'specs' && (
            <table className={styles.specsTable}>
              <tbody>
                <tr>
                  <td className={styles.specsLabel}>Model</td>
                  <td>{product.productName}</td>
                </tr>
                <tr>
                  <td className={styles.specsLabel}>Seçili Renk</td>
                  <td>{selectedColor || 'Standart'}</td>
                </tr>
                <tr>
                  <td className={styles.specsLabel}>Stok Miktarı</td>
                  <td>{product.productStock ?? 'Stokta mevcut'}</td>
                </tr>
                <tr>
                  <td className={styles.specsLabel}>Garanti Süresi</td>
                  <td>24 Ay Resmi Distribütör Garantisi</td>
                </tr>
              </tbody>
            </table>
          )}

          {activeTab === 'shipping' && (
            <div>
              <p>
                <strong>Kargo Bilgileri:</strong> Saat 16:00&apos;ya kadar verilen siparişler aynı gün kargoya teslim edilir. Tüm siparişlerde kargo ücretsizdir.
              </p>
              <p style={{ marginTop: '0.75rem' }}>
                <strong>İade Koşulları:</strong> Ürünü ambalajı açılmamış ve faturası ile birlikte 14 gün içinde koşulsuz iade edebilirsiniz.
              </p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default ProductDetailPage;
