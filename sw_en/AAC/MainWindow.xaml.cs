using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MATERIAL;
using CRSC;

namespace AAC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Combobox input

        int selected_AAC_ComponentIndex;
        int selected_StandardIndex;
        int selected_SupportMaterial_1S_Index;
        int selected_SupportMaterial_2E_Index;
        int selected_AAC_StrengthClassIndex;
        int selected_AAC_DensityClassIndex;
        int selected_Reinforcement_StrengthClassIndex;
        int selected_Reinforcement_d_long_upper_Index;
        int selected_Reinforcement_d_long_lower_Index;
        int selected_Reinforcement_d_trans_Index;

        float fg_grav_acc_constant = 10.0f; // Gravitational acceleration constant
        PanelPreview model = new PanelPreview();
        MATERIAL.CMat_02_00_AAC Concrete = new MATERIAL.CMat_02_00_AAC();
        MATERIAL.CMat_03_00 Reinforcement = new MATERIAL.CMat_03_00();
        AAC_Database_Data AAC_data = new AAC_Database_Data();

        // Geometry

        float fh = 0.0f;   // Panel Height
        float fb = 0.0f;   // Panel Width
        float fL_w = 0.0f; // Length between supports
        float fL = 0.0f; // Panel Length
        float fa_1 = 0.0f; // Support Length at the Start
        float fa_1_min = 0.0f; // Support Minimum Length
        float fa_2_min = 0.0f; // Support Minimum Length
        float fc_1 = 0.0f; // Concrete Cover
        float fc_2 = 0.0f; // Concrete Cover
        float fc_trans = 0.0f; // Concrete Cover of transversal reinforcement (distance between end of bar and concrete surface)

        // Concrete
        float fFactor_Alpha = 0.0f; // Reduction coefficient for long term effect on compressive strength of concrete

        public float fGamma_c = 0.0f;

        // Reinforcement

        int number_long_upper_bars = 0;
        int number_long_lower_bars = 0;
        int number_trans_bars = 0;

        public float d_long_upper = 0.0f;
        public float d_long_lower = 0.0f;
        public float d_trans = 0.0f;

        public float b_long_upper = 0.0f;
        public float b_long_lower = 0.0f;

        public float fGamma_s = 0.0f;

        // Loading

        float fgamma_g = 0.0f;
        float fgamma_q = 0.0f;
        float fPsi_1 = 0.0f;
        float fPsi_2 = 0.0f;
        float fg_k = 0.0f;
        float fq_k = 0.0f;

        // Transport

        float fb_s = 0.0f;
        float fgamma_t = 0.0f;
        float fRho_trans = 0.0f;



        private void ComboBox_AAC_Component_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get control that raised this event.
            ComboBox combobox = (ComboBox)sender;
            selected_AAC_ComponentIndex = combobox.SelectedIndex;
        }

        private void ComboBox_Standard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get control that raised this event.
            ComboBox combobox = (ComboBox)sender;
            selected_StandardIndex = combobox.SelectedIndex;
        }

        private void ComboBox_SupportMaterial_1S_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get control that raised this event.
            ComboBox combobox = (ComboBox)sender;
            selected_SupportMaterial_1S_Index = combobox.SelectedIndex;
        }

        private void ComboBox_SupportMaterial_2E_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get control that raised this event.
            ComboBox combobox = (ComboBox)sender;
            selected_SupportMaterial_2E_Index = combobox.SelectedIndex;
        }

        private void TextBoxLength_h_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fh = (float)Convert.ToDecimal(textBox.Text);

            //Update preview
            model.CreatePreviewModel(fL, fb, fh);
        }

        private void TextBoxLength_b_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fb = (float)Convert.ToDecimal(textBox.Text);

            //Update preview
            model.CreatePreviewModel(fL, fb, fh);
        }

        private void TextBoxLength_Lw_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fL_w = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxLength_L_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fL = (float) Convert.ToDecimal(textBox.Text);

            //Update preview
            model.CreatePreviewModel(fL, fb, fh);
        }

        private void TextBoxLength_a1_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fa_1 = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxLength_c1_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fc_1 = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxLength_c2_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fc_2 = (float)Convert.ToDecimal(textBox.Text);
        }

        private void ComboBox_AAC_CSC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get control that raised this event.
            ComboBox combobox = (ComboBox)sender;
            selected_AAC_StrengthClassIndex = combobox.SelectedIndex;
        }

        private void ComboBox_AAC_DC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get control that raised this event.
            ComboBox combobox = (ComboBox)sender;
            selected_AAC_DensityClassIndex = combobox.SelectedIndex;
        }

        private void TextBoxConcreteDensity_rho_m_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            Concrete.Rho_m = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxConcreteFactor_Alpha_c_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fFactor_Alpha = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxConcreteFactor_Gamma_c_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fGamma_c = (float)Convert.ToDecimal(textBox.Text);
        }

        // Reinforcement

        private void ComboBox_Reinforcement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get control that raised this event.
            ComboBox combobox = (ComboBox)sender;
            selected_Reinforcement_StrengthClassIndex = combobox.SelectedIndex;
        }

        private void TextBoxLongReinUpper_No_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            number_long_upper_bars = Convert.ToInt32(textBox.Text);
        }

        private void TextBoxLongReinLower_No_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            number_long_lower_bars = Convert.ToInt32(textBox.Text);
        }

        private void TextBoxTransRein_No_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            number_trans_bars = Convert.ToInt32(textBox.Text);
        }

        private void ComboBox_LongReinUpper_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get control that raised this event.
            ComboBox combobox = (ComboBox)sender;
            selected_Reinforcement_d_long_upper_Index = combobox.SelectedIndex;
        }

        private void TextBoxLongReinUpper_distance_b_2_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            b_long_upper = (float)Convert.ToDecimal(textBox.Text);
        }

        private void ComboBox_LongReinLower_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get control that raised this event.
            ComboBox combobox = (ComboBox)sender;
            selected_Reinforcement_d_long_lower_Index = combobox.SelectedIndex;
        }

        private void TextBoxLongReinLower_distance_b_1_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            b_long_lower = (float)Convert.ToDecimal(textBox.Text);
        }

        private void ComboBox_TransRein_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get control that raised this event.
            ComboBox combobox = (ComboBox)sender;
            selected_Reinforcement_d_trans_Index = combobox.SelectedIndex;
        }

        private void TextBoxReinforcementFactor_Gamma_s_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fGamma_s = (float)Convert.ToDecimal(textBox.Text);
        }

        // Loading

        private void TextBoxLoadingFactorGamma_g_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fgamma_g = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxLoadingFactorGamma_q_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fgamma_q = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxLoadingFactorPsi_1_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fPsi_1 = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxLoadingFactorPsi_2_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fPsi_2 = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxLoadingFactorValue_g_k_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fg_k = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxLoadingFactorValue_q_k_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fq_k = (float)Convert.ToDecimal(textBox.Text);
        }

        // Transport

        private void TextBoxTransportValue_b_s_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fb_s = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxTransportValue_Gamma_t_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fgamma_t = (float)Convert.ToDecimal(textBox.Text);
        }

        private void TextBoxAACPanelDensity_Rho_trans_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ... Get control that raised this event.
            var textBox = sender as TextBox;
            fRho_trans = (float)Convert.ToDecimal(textBox.Text);
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            CRSC.CCrSc_2_00 Cross_Section = new CRSC.CCrSc_2_00(fh, fb);

            Concrete.FillData(selected_AAC_StrengthClassIndex, Concrete.Rho_m);

            AAC_data.Get_Database_Data(selected_Reinforcement_StrengthClassIndex,
                                 selected_Reinforcement_d_long_upper_Index,
                                 selected_Reinforcement_d_long_lower_Index,
                                 selected_Reinforcement_d_trans_Index,
                                 out Reinforcement.m_ff_yk_0,
                                 out d_long_upper,
                                 out d_long_lower,
                                 out d_trans);

            // Floor Panel EN 12602


            float fa_2 = (fL - fL_w - fa_1); // Todo predpoklada symetricke ulozenie na podpory

            fa_1_min = Get_minimum_support_a_min(selected_AAC_ComponentIndex, selected_SupportMaterial_1S_Index);
            fa_2_min = Get_minimum_support_a_min(selected_AAC_ComponentIndex, selected_SupportMaterial_2E_Index);

            float fL_eff = fL_w + 1.0f / 3.0f * fa_1_min + 1.0f / 3.0f * fa_2_min;
            
            float fd_eff_lower = fh - fc_1 - 0.5f * d_long_lower; // Effective Depth
            float fb_load = fb; // !!!!!! ????? Could be different

            float fA_c_lower = fb * fd_eff_lower; // Compressed area of concrete
            float fn_p_support = 2.0f;

            fc_trans = 0.02f; // Mozno presunut do zadavania

            // Internal forces

            fg_k *= 1000.0f; // N / m^2
            fq_k *= 1000.0f; // N / m^2

            // Characteristic combinations
            float fG_d1 = fgamma_g * fb_load * fg_k;
            float fQ_d1 = fgamma_q * fb_load * fq_k;

            float fV_Sd_1 = 0.5f * (fG_d1 + fQ_d1) * fL_eff;
            float fM_Sd_1 = ((fG_d1 + fQ_d1) * fL_eff * fL_eff) / 8.0f;

            // Frequent combinations
            float fG_d2 = fb_load * fg_k;
            float fQ_d2 = fPsi_1 * fb_load * fq_k;

            float fV_Sd_2 = 0.5f * (fG_d2 + fQ_d2) * fL_eff;
            float fM_Sd_2 = ((fG_d2 + fQ_d2) * fL_eff * fL_eff) / 8.0f;

            // Quasi-permanent combinations
            float fG_d3 = fb_load * fg_k;
            float fQ_d3 = fPsi_2 * fb_load * fq_k;

            float fV_Sd_3 = 0.5f * (fG_d3 + fQ_d3) * fL_eff;
            float fM_Sd_3 = ((fG_d3 + fQ_d3) * fL_eff * fL_eff) / 8.0f;

            // Internal forces for transport situations
            float fG_t = fgamma_g * fg_grav_acc_constant * fRho_trans * Cross_Section.Fb * Cross_Section.Fh;

            // Cantilever
            float fL_cantilever = 0.5f * (fL - fb_s);
            float fV_t = fgamma_t * fG_t * fL_cantilever;
            float fM_t = (float)(0.5f * fgamma_t * fG_t * Math.Pow(fL_cantilever, 2));

            // Design
            double fTau_Rd = 0.063f * Math.Sqrt(Concrete.Fck) / Concrete.D_gamaMc;
            Concrete.D_Ecm = 5.0 * (Concrete.Rho_m - 150.0f);
            double thousand_md = 1000 * fM_Sd_1 * Concrete.D_gamaMc / (fFactor_Alpha * Concrete.Fck * fA_c_lower * fd_eff_lower);

            // Annex A
            AAC_data.GetAAC_values_for_1000md(thousand_md);

            double fEpsilon_c = AAC_data.AAC_value_array_for_1000md[0];
            double fEpsilon_s = AAC_data.AAC_value_array_for_1000md[1];
            double fk_x = AAC_data.AAC_value_array_for_1000md[2];
            double fk_z = AAC_data.AAC_value_array_for_1000md[3];
            double thousand_omega_S235 = AAC_data.AAC_value_array_for_1000md[4];
            double thousand_omega_S500 = AAC_data.AAC_value_array_for_1000md[5];
            double thousand_omega = 0.0;

            if (Reinforcement.m_ff_yk_0 <= 2.51e+8)
                thousand_omega = thousand_omega_S235;
            else
                thousand_omega = thousand_omega_S500;

            float fomega = (float)thousand_omega / 1000.0f;

            double fA_s_b_min = fA_c_lower * fomega * fFactor_Alpha * Concrete.Fck * fGamma_s / (fGamma_c * Reinforcement.m_ff_yk_0);

            // Upper Reinforcement
            float fd_eff_u = fh - fc_2 - 0.5f * d_long_upper; // Effective Depth
            float fA_c_u = fb * fd_eff_u; // Compressed area of concrete

            double thousand_md_upper = 1000 * fM_t * fGamma_c / (fFactor_Alpha * Concrete.Fck * fA_c_u * fd_eff_u);

            AAC_data.GetAAC_values_for_1000md(thousand_md_upper);

            double fEpsilon_c_u = AAC_data.AAC_value_array_for_1000md[0];
            double fEpsilon_s_u = AAC_data.AAC_value_array_for_1000md[1];
            double fk_x_u = AAC_data.AAC_value_array_for_1000md[2];
            double fk_z_u = AAC_data.AAC_value_array_for_1000md[3];
            double thousand_omega_S235_u = AAC_data.AAC_value_array_for_1000md[4];
            double thousand_omega_S500_u = AAC_data.AAC_value_array_for_1000md[5];
            double thousand_omega_u = 0.0;

            if (Reinforcement.m_ff_yk_0 <= 2.51e+8)
                thousand_omega_u = thousand_omega_S235_u;
            else
                thousand_omega_u = thousand_omega_S500_u;

            float fomega_u = (float)thousand_omega_u / 1000.0f;

            double fA_s_u_min = fA_c_u * fomega_u * fFactor_Alpha * Concrete.Fck * fGamma_s / (fGamma_c * Reinforcement.m_ff_yk_0);

            // Minimum Reinforcement

            double ff_cflm = 0.27f * /*0.8f **/ Concrete.Fck; // flexural strength

            float fA_ct = Cross_Section.Fb * 0.5f * Cross_Section.Fh;

            double fAs_min = fk_x_u * fA_ct * ff_cflm / Reinforcement.m_ff_yk_0;

            double fA_s_exis_lower = number_long_lower_bars * Math.PI * (d_long_lower * d_long_lower / 4.0); // Bottom Reinforcement
            double fA_s_exis_upper = number_long_upper_bars * Math.PI * (d_long_upper * d_long_upper / 4.0); // Upper Reinforcement

            float fb_w = Cross_Section.Fb; //  ??????? Moze byt ina

            double rho_l = fA_s_exis_lower / (Cross_Section.Fb * fd_eff_lower);
            double VRd_1_min = 0.5f * Concrete.Fctk0_05 / fGamma_c * fb_w * fd_eff_lower;
            double VRd_1 = fTau_Rd * (1.0f - 0.83f * fd_eff_lower) * (1 + 240 * rho_l) * fb_w * fd_eff_lower;

            // Transverse reinforcement bars
            double fe = fc_1 + d_long_lower + 0.5f * d_trans;

            // Maximum tensile force
            double z = 0.9f * fd_eff_lower; // 0.9 ????
            float fM_d1_max = fM_Sd_1;

            double F_ld_max = fM_d1_max / z;

            float fd_support = Math.Max(fa_1, fa_2); // Ulozenie na podpore ???

            float fM_d1_support = fV_Sd_1 * fd_support;
            double F_ld_support = fM_d1_support / z;

            // Design value of bearing strength at support
            float fm_support = 1.3f;
            double f_ld_support = 1.35f * fm_support * Math.Pow(fe / d_trans, 1.0 / 3.0) * fFactor_Alpha * Concrete.Fck / fGamma_c;
            double f_ld_limit = 2.2f * Concrete.Fck / fGamma_c;

            // Design value of bearing strength at middle of span
            float fm_midspan = 1.067f;
            float fn_p_midspan = 2.0f;
            double f_ld_midspan = 1.35f * fm_midspan * Math.Pow(fe / d_trans, 1.0 / 3.0) * fFactor_Alpha * Concrete.Fck / fGamma_c;

            // Anchorage force capacity - bottom reinforcement
            float fA_sl = 0.0f;
            float fn_l = number_long_lower_bars;
            float fn_t = 2.0f;

            float t_t = (number_long_lower_bars - 2) * b_long_lower + 2 * 0.5f * b_long_lower + 0.015f + 0.015f; // Vzdialenost medzi pozdlznou vystuzou + presah // TODO

            double fF_RA_support = 0.83f * fn_t * d_trans * t_t * f_ld_support;
            float fF_wg = 0.25f * fA_sl * Reinforcement.m_ff_yk_0;
            double fF_RA_support_limit = 0.6f * fn_l * fn_t * fF_wg / fGamma_s;

            double fF_RA_max = 0.0f;

            // Anchorage force capacity - upper reinforcement

            // doplnit


            // Serviceability Limit States

            // Cracking moment
            //float ff_cflm = 0.27f * 0.8f * Concrete.Fck; // flexural strength 0.8  ?????

            Cross_Section.FW_y_el = (float)(Cross_Section.Fb * Math.Pow(Cross_Section.Fh, 2) / 6);

            double M_cr = Cross_Section.FW_y_el * ff_cflm;

            float fd_1 = d_long_lower;
            float fd_2 = d_long_upper;

            double A_s1 = fA_s_exis_lower;
            double A_s2 = fA_s_exis_upper;

            float fy_s1 = fc_1 + 0.5f * d_long_lower;
            float fy_s2 = Cross_Section.Fh - fc_2 + 0.5f * d_long_upper - d_trans; // Horna vyztuz sa nachadza pod priecnou ??? TODO overit podla vykresov

            // Deflection under uncracked condition
            // Short-term deflection
            // Ratio of the modulus of elasticity of reinforcing steel and AAC

            Reinforcement.m_fE = 2.0e11f;
            float fn_short_term = (float)(Reinforcement.m_fE / Concrete.E_cm);
            fn_short_term = 100.0f; // approximately - short term condition

            double fI_c_brutto_shortterm = Get_I_c_brutto(Cross_Section.Fb, Cross_Section.Fh, fn_short_term, number_long_lower_bars, fd_1, number_long_upper_bars, fd_2); // Moment of inertia of AAC and reinforcement
            double y_s_shortterm = Get_y_s(Cross_Section.Fb, Cross_Section.Fh, fn_short_term, fy_s1, fy_s2, A_s1, A_s2); // Centre of grafity
            double I_st_shortterm = Get_I_st(Cross_Section.Fb, Cross_Section.Fh, fn_short_term, y_s_shortterm, fy_s1, fy_s2, A_s1, A_s2); // Moment of inertia of reinforcement
            double I_ci_shortterm = fI_c_brutto_shortterm + I_st_shortterm;
            double Ecm_Ici_shortterm = Concrete.D_Ecm * I_ci_shortterm;
            double y_el_shortterm = 5.0f / 48.0f * fM_Sd_2 * Math.Pow(fL_eff, 2.0f) / (Ecm_Ici_shortterm); // Deflection due to load combination 2
            double y_el_lim = fL_eff / 250.0f;

            // Long-term deflection
            float fPhi = 1.0f; // Todo - temporary
            float fE_c_eff = (float)Concrete.E_cm / (1 + fPhi);
            fE_c_eff = 1000000000.0f;
            float fn_long_term = 200.0f; // approximately - long term condition

            double fI_c_brutto_longterm = Get_I_c_brutto(Cross_Section.Fb, Cross_Section.Fh, fn_long_term, number_long_lower_bars, fd_1, number_long_upper_bars, fd_2);
            double y_s_longterm = Get_y_s(Cross_Section.Fb, Cross_Section.Fh, fn_long_term, fy_s1, fy_s2, A_s1, A_s2);
            double I_st_longterm = Get_I_st(Cross_Section.Fb, Cross_Section.Fh, fn_long_term, y_s_longterm, fy_s1, fy_s2, A_s1, A_s2);
            double I_ci_longterm = fI_c_brutto_longterm + I_st_longterm;
            double Eceff_Ici_longterm = fE_c_eff * I_ci_longterm;
            double y_el_longterm = 5.0f / 48.0f * fM_Sd_3 * Math.Pow(fL_eff, 2.0f) / (Eceff_Ici_longterm);

            // Deflection under cracked condition
            // Short-term deflection

            double fA = fb * Concrete.E_cm/ (2.0f * A_s1 * Reinforcement.m_fE);
            double x = (Math.Sqrt(1.0f + 4.0f * fd_eff_lower * fA) - 1.0f) / (2.0f * fA);

            double fI_c_brutto_shortterm_crk = Get_I_c_brutto(Cross_Section.Fb, x, fn_short_term, number_long_lower_bars, fd_1, number_long_upper_bars, fd_2);
            double y_s_shortterm_crk = Get_y_s_x(fb, fh, x, fn_short_term, fy_s1, fy_s2, A_s1, A_s2);
            double I_st_shortterm_crk = Get_I_st_x(Cross_Section.Fb, Cross_Section.Fh, x, fn_short_term, y_s_shortterm, fy_s1, fy_s2, A_s1, A_s2);
            double I_ci_shortterm_crk = fI_c_brutto_shortterm_crk + I_st_shortterm_crk;
            double Ecm_Ici_shortterm_crk = Concrete.D_Ecm * I_ci_shortterm_crk;
            double y_el_shortterm_crk = 5.0f / 48.0f * fM_Sd_2 * Math.Pow(fL_eff, 2.0f) / (Ecm_Ici_shortterm_crk);

            // Long-term deflection
            double fI_c_brutto_longterm_crk = Get_I_c_brutto(Cross_Section.Fb, x, fn_long_term, number_long_lower_bars, fd_1, number_long_upper_bars, fd_2);
            double y_s_longterm_crk = Get_y_s_x(fb, fh, x, fn_long_term, fy_s1, fy_s2, A_s1, A_s2);
            double I_st_longterm_crk = Get_I_st_x(Cross_Section.Fb, Cross_Section.Fh, x, fn_long_term, y_s_longterm_crk, fy_s1, fy_s2, A_s1, A_s2);
            double I_ci_longterm_crk = fI_c_brutto_longterm_crk + I_st_longterm_crk;
            double Eceff_Ici_longterm_crk = fE_c_eff * I_ci_longterm_crk;
            double y_el_longterm_crk = 5.0f / 48.0f * fM_Sd_3 * Math.Pow(fL_eff, 2.0f) / (Eceff_Ici_longterm_crk);

            // Combination of deflection uncracked / cracked

            double k = 1.0f - 0.8f * Math.Pow(M_cr / fM_Sd_2, 2);

            // Short-term deflection
            double y_el_comb_shortterm = Get_y_el_comb(k, y_el_shortterm, y_el_shortterm_crk);
            // Long-term deflection
            double y_el_comb_longterm = Get_y_el_comb(k, y_el_longterm, y_el_longterm_crk);
        }

        // Private Auxiliary Functions
        private double Get_I_c_brutto (float fb, double x, float n, int i_number_1, float fd_1, int i_number_2, float fd_2)
        {
            return 1.0f / 12.0f * fb * Math.Pow(x, 3.0) + n * (i_number_1 * Math.PI * 0.25f * Math.Pow(0.5f * fd_1, 4) + i_number_2 * Math.PI * 0.25f * Math.Pow(0.5f * fd_2, 4));
        }

        private double Get_y_s(float fb, float fh, float n, float fy_s1, float fy_s2, double A_s1, double A_s2)
        {
            return (fb * fh * 0.5f * fh + n * (A_s1 * fy_s1 + A_s2 * fy_s2)) / (fb * fh + n * (A_s1 + A_s2));
        }

        private double Get_y_s_x(float fb, float fh, double x, float n, float fy_s1, float fy_s2, double A_s1, double A_s2)
        {
            return (fb * x * (fh - 0.5f * x) + n * (A_s1 * fy_s1 + A_s2 * fy_s2)) / (fb * x + n * (A_s1 + A_s2));
        }

        private double Get_I_st(float fb, float fh, float n, double y_s, float fy_s1, float fy_s2, double A_s1, double A_s2)
        {
            return fb * fh * Math.Pow(0.5f * fh - y_s, 2.0f) + n * (A_s1 * Math.Pow(fy_s1 - y_s, 2.0f) + A_s2 * Math.Pow(fy_s2 - y_s, 2.0f));
        }

        private double Get_I_st_x(float fb, float fh, double x, float n, double y_s, float fy_s1, float fy_s2, double A_s1, double A_s2)
        {
            return fb * x * Math.Pow(fh - 0.5f * x - y_s, 2.0f) + n * (A_s1 * Math.Pow(fy_s1 - y_s, 2.0f) + Math.Pow(fy_s2 - y_s, 2.0f));
        }

        private double Get_y_el_comb(double k, double fp_I, double fp_II)
        {
            return k * fp_II + (1.0f - k) * fp_I;
        }

        public float Get_minimum_support_a_min(int selected_AAC_ComponentIndex, int selected_SupportMaterialIndex)
        {
            //AAC element

            //0 - "Floor Panel"
            //1 - "Roof Panel"
            //2 - "Vertical Wall Panel"
            //3 - "Horizontal Wall Panel"
            //4 - "Beam"

            //Support Material

            //0 - "masonry"
            //1 - "steel"
            //2 - "concrete"
            //3 - "wood"

            if (selected_AAC_ComponentIndex == 4)
                return 0.10f;
            else if (selected_SupportMaterialIndex == 0)
                return 0.07f;
            else
                return 0.05f;
        }



    }

    public class ComboItem
    {
        public string ItemText { get; set; }
        public float ItemFloat { get; set; }

        public ComboItem(string itemText, float itemFloat)
        {
            this.ItemText = itemText;
            this.ItemFloat = itemFloat;
        }

        public override string ToString()
        {
            return this.ItemText;
        }
    }
}
