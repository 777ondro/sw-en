using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace M_EC2.SOLV.CS
{
    class CL_62  // Clause 6.2 Shear
    {
        float Eq_61______(float fV_Rd_s, float fV_ccd, float fV_td)
        {
            return fV_Rd_s + fV_ccd + fV_td; // Eq. (6.1) fV_Rd
        }
        float Eq_62a_____(float fC_Rd_c, float fk, float frho_1, float ff_ck, float fk_1, float fsigma_cp, float fb_w, float fd)
        {
            return (fC_Rd_c * fk * (float)Math.Pow((100f * frho_1 * ff_ck), 1f / 3f) + fk_1 * fsigma_cp) * fb_w * fd; // Eq. (6.2a) fV_Rd_c
        }
        double Eq_62b_____(double fv_min, double fk_1, double fsigma_cp, double fb_w, double fd)
        {
            return (fv_min + fk_1 * fsigma_cp) * fb_w * fd; // Eq. (6.2b) fV_Rd_c
        }
        double Eq_k_______(double fd)
        {
            if (fd > 0f)
                return Math.Min((1f + Math.Sqrt(200f / (fd * 1000f))), 2f); // Eq. k, dd - in [mm]
            else
                return 0f;
        }
        float Eq_rho_1___(float fA_s1, float fb_w, float fd)
        {
            if (fb_w > 0f && fd > 0f)
                return Math.Min(fA_s1 / (fb_w * fd), 0.02f); // Eq. rho_1
            else
                return 0f;
        }
        float Eq_sig_cp_62(float fN_Ed, float fA_c, float ff_cd)
        {
            if (fA_c > 0f &&  ff_cd > 0f)
                return Math.Min(Math.Abs(fN_Ed) / fA_c, 0.2f * ff_cd); // Eq. sigma_cp for (6.2)
            else
                return 0f;
        }               
        float Eq_64______(float fI, float fb_w, float fS, float ff_ctd, float falpha_l, float fsigma_cp)
        {
            if (fS > 0f && fb_w > 0f && ff_ctd > 0f)
                return ((fI * fb_w) / fS) * (float)Math.Sqrt(Math.Pow(ff_ctd,2f)) + falpha_l * fsigma_cp * ff_ctd; // Eq. V_Rd_c
            else
                return 0f;
        }
        float Eq_aplha_l__(float dI_x, float dI_pt2, bool b_pressed_r)
        {
            if (b_pressed_r)
            {
                if (dI_pt2 > 0f)
                    return Math.Min(dI_x / dI_pt2, 1f); // Eq. alpha_l
                else
                    return 0f;
            }
            else
                return 1f; // not presressed reinforcemet
        }
        float Eq_sig_cp_64(float dN_Ed, float dA_c)
        {
            if (dA_c > 0f)
                return Math.Abs(dN_Ed) / dA_c; // Eq. sigma_cp for (6.4)
            else
                return 0f;
        }
        float Eq_65______(float fV_Ed, float fb_w, float fd, float fnu, float ff_cd)
        {
            if (fb_w > 0f && fd > 0f && fnu > 0f && ff_cd > 0f)
                return fV_Ed / (0.5f * fb_w * fd * fnu * ff_cd); // Eq. (6.5) ratio 
            else
                return 0f;
        }
        float Eq_66N_____(float fnu, float ff_ck)
        {
            return 0.6f * (1f - ff_ck / 250e+6f); // Eq. (6.5) nu 
        }
        float Eq_67N_____(float f_cot_phi,float f_cot_phi_min /*= 1*/, float f_cot_phi_max /*=2.5*/)
        {
            if (f_cot_phi_min <= f_cot_phi && f_cot_phi <= f_cot_phi_max)
                return f_cot_phi; // Eq. (6.7) cot phi
            else
                return Math.Max(f_cot_phi_min, Math.Min(f_cot_phi, f_cot_phi_max));
        }






        /////
        // Tu Pokracujeme s EN 1992-1-1. Dakujem. Mato
        /////








        // nazov  funkcie ktora vracia vysledok rovnice je Eq_ + 8 znakov (cislo rovnice v norme)
        // prazdne miesta vzdy vyplnit ako podtrznik
        // Eq_XXXXXXXX
        // Eq_61______
        // Eq_61656a__

        // Typy premennych:

        // double - realne číslo - vyssia presnost
        // float - realne cislo - nizsia presnost - preferujeme
        // decimal - nepouzivame
        // int  - cele čislo

        // prve pismeno premennej je podla jej typu " d, f i ..."

        // return - vracia výsledok funkcie
        // kontrolovat (if,elseif,else) ci su cisla v menovateloch ine ako 0 ( < > <= >=  == != ... ak maju byt kladne, ci su kladne) inak vratit return 0
        // prikazove riadky kocia bodkociarkou";"
        // vo vypise sa vstupne premenne oddeluju ciarkou ","
        // dolezite je aby boli spravne dvojice zatvoriek ()

        // (float)Math.Pow(,) - zapis znamena ze vysledok funkcie Pow (druha mocnina), ktora vracia hodnotu typu double sa skonvertuje na float


// vypis znakov greckej abecedy - v rovniciach pouzivat anglicky nazov namiesto symbolu
string [ , ] aGreekAlphabet = 
{
    {"Α","α","Alpha     "},	
    {"Β","β","Beta	    "},
    {"Γ","γ","Gamma	    "},
    {"Δ","δ","Delta	    "},
    {"Ε","ε","Epsilon   "},	
    {"Ζ","ζ","Zeta	    "},
    {"Η","η","Eta	    "},
    {"Θ","θ","Theta	    "},
    {"Ι","ι","Iota	    "},
    {"Κ","κ","Kappa	    "},
    {"Λ","λ","Lambda	"},
    {"Μ","μ","Mu	    "},
    {"Ν","ν","Nu	    "},
    {"Ξ","ξ","Xi	    "},
    {"Ο","ο","Omicron	"},
    {"Π","π","Pi	    "},
    {"Ρ","ρ","Rho  	    "},
    {"Σ","σ","Sigma	    "},
    {"Τ","τ","Tau	    "},
    {"Υ","υ","Upsilon	"},
    {"Φ","φ","Phi	    "},
    {"Χ","χ","Chi	    "},
    {"Ψ","ψ","Psi	    "},
    {"Ω","ω","Omega     "}
} ;                  

}
}
